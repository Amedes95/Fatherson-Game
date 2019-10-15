using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D playerBody;
    public float playerSpeed;
    public static float jumpForce;
    public float counterJumpForce;
    private float fallVelocity;
    private Animator PlayerAnim;
    public float jumpHeight;
    public static bool isJumping;
    bool jumpKeyHeld;
    public static int jumpCount;
    public float fallForce;
    public float fallSpeedCap;
    public float startSpeed;
    public float midSpeed;
    public float fullSpeed;
    public float maxVelocity;
    public float midVelocity;
    public static bool Sprinting;
    public Transform wallEndLine;
    bool touchingWall;
    bool wallClinging;
    int playerDirection;
    Vector2 walljump;

    Transform PlayerSpawn; // passed in through editor

    void Awake()
    {
        PlayerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").GetComponent<Transform>();
        playerBody = GetComponent<Rigidbody2D>();
        PlayerAnim = GetComponent<Animator>();
        transform.position = PlayerSpawn.position; // spawn
        startSpeed = 60;
        midSpeed = 18;
        fullSpeed = 14; //full speed should always be less than mid speed
        maxVelocity = 5;
        midVelocity = 3;


    }

    public static float CalculateJumpForce(float playerGravity, float jumpHeight)
    {
        return Mathf.Sqrt(2 * playerGravity * jumpHeight);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerHealth.Dead == false) // Only allow movement if alive
        {
            float moveHorizontal = Input.GetAxis("Horizontal"); // left is -1, stopped is 0, right is 1
            fallVelocity = playerBody.velocity.y;
            jumpForce = CalculateJumpForce(playerBody.gravityScale, jumpHeight);

            Vector2 movementPlayer = new Vector2(moveHorizontal, 0);

            if (transform.localScale.x > 0)
            { playerDirection = 1; }
            else
            { playerDirection = -1; }

            walljump = new Vector2(-playerDirection, 1);
            if (playerBody.velocity.y < -1)
            {
                playerBody.AddForce(new Vector2(0, -1) * fallForce);
            }

            ///// Sprinting
            if (Sprinting && JumpDetector.OnGround)
            {
                PlayerAnim.SetFloat("SprintSpeed", 2);
                startSpeed = 200;
                midSpeed = 26;
                fullSpeed = 20;
                maxVelocity = 8;
                midVelocity = 5;
            }

            if (!Sprinting && !JumpDetector.OnGround)
            {
                PlayerAnim.SetFloat("SprintSpeed", 1);
                startSpeed = 60;
                midSpeed = 18;
                fullSpeed = 14;
                maxVelocity = 4;
                midVelocity = 3;
            }

            if (touchingWall && !JumpDetector.OnGround)
            {
                if (playerBody.velocity.y < -.5f)
                {
                    isJumping = false;
                    playerBody.velocity = new Vector2(playerBody.velocity.x, -.5f);
                }
            }


            if (Mathf.Abs(playerBody.velocity.x) > maxVelocity && JumpDetector.OnGround) //ground speed cap
            {
                playerSpeed = fullSpeed;
            }
            else if (Mathf.Abs(playerBody.velocity.x) < midVelocity && JumpDetector.OnGround) //quick movement from rest
            {
                playerSpeed = startSpeed;
            }
            else if (JumpDetector.OnGround)
            {
                playerSpeed = midSpeed;
            }

            if (Mathf.Abs(playerBody.velocity.x) > maxVelocity && !JumpDetector.OnGround) //air speed cap
            {
                playerBody.AddForce(movementPlayer * playerSpeed * -1);
            }




            if (fallVelocity < -fallSpeedCap) //fall speed cap
            {
                playerBody.AddForce(Vector2.up * playerBody.gravityScale * 10);
            }

            ///// Movement left and right
            if (moveHorizontal > 0f) // player is moving right
            {
                playerBody.AddForce(movementPlayer * playerSpeed);
                PlayerAnim.SetBool("Running", true);
            }
            if (moveHorizontal < 0f) // player is moving left
            {
                playerBody.AddForce(movementPlayer * playerSpeed);
                PlayerAnim.SetBool("Running", true);
            }

            if (Mathf.Abs(moveHorizontal) < .01) // player is stopped
            {
                PlayerAnim.SetBool("Running", false);
            }

            if (isJumping)
            {
                if (!jumpKeyHeld && Vector2.Dot(playerBody.velocity, Vector2.up) > 0)
                {
                    playerBody.AddForce(counterJumpForce * playerBody.mass * Vector2.down);
                }
            }
        }
    }

    public void Jump()
    {
        Vector2 jump = Vector2.up;
        playerBody.AddForce(jump * jumpForce * 180);
        PlayerAnim.SetTrigger("Jump");
        isJumping = true;
    }

    public void WallJump()
    {
        playerBody.AddForce(walljump * jumpForce * 180);
        isJumping = true;
    }

    public void WallRaycasting()
    {
        Debug.DrawLine(transform.position, wallEndLine.position, Color.green);  // during playtime, projects a line from a start point to and end point
        touchingWall = Physics2D.Linecast(transform.position, wallEndLine.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void Update()
    {
        if (PlayerHealth.Dead == false) // Only allow inputs if alive
        {
            //// JUMPING
            if (Input.GetKeyDown(KeyCode.W))
            {
                jumpKeyHeld = true;

                if (!JumpDetector.OnGround && touchingWall) //Walljump
                {
                    WallJump();
                }

                if (!JumpDetector.OnGround && jumpCount > 0 && !touchingWall) //Double jump
                {
                    playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
                    Jump();
                    jumpCount -= 1;
                }

                if (JumpDetector.OnGround) // Checks to see if player is on ground before jumping
                {
                    Jump();
                }
                
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                jumpKeyHeld = false;
            }

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                Sprinting = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                Sprinting = false;  
            }

            //// Walljumping
            WallRaycasting();

            ////Face direction of horizontal movement
            if (playerBody.velocity.x > .5f)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                Debug.Log("Flip");
            }

            if (playerBody.velocity.x < -.5f)
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                Debug.Log("Flip");
            }
        }
    }

    public void Respawn()
    {
        //moves the player to SpawnPosition
        transform.position = PlayerSpawn.position;
    }
}

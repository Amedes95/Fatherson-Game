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
    public static bool wallJumping;
    public static float jumpFallCooldown;
    public static bool recentlyJumped;
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
    int playerDirection;
    Vector2 walljumpVector;
    bool isFloating;
    public float floatingTimer;
    public float playerGravity;



    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        PlayerAnim = GetComponent<Animator>();
        startSpeed = 60;
        midSpeed = 18;
        fullSpeed = 14; //full speed should always be less than mid speed
        maxVelocity = 5;
        midVelocity = 3;
        floatingTimer = -1;
        playerGravity = 2;


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

            walljumpVector = new Vector2(-playerDirection, 1);
            if (playerBody.velocity.y < -1)
            {
                playerBody.AddForce(new Vector2(0, -1) * fallForce);
            }

            ///// Sprinting
            if (Sprinting)
            {
                PlayerAnim.SetFloat("SprintSpeed", 2);
                startSpeed = 200;
                midSpeed = 26;
                fullSpeed = 20;
                maxVelocity = 8;
                midVelocity = 5;
            }

            if (!Sprinting)
            {
                PlayerAnim.SetFloat("SprintSpeed", 1);
                startSpeed = 60;
                midSpeed = 18;
                fullSpeed = 14;
                maxVelocity = 4;
                midVelocity = 3;
            }

            if (touchingWall && !JumpDetector.OnGround) //Wall Slide
            {
                if (playerBody.velocity.y < 0)
                {
                    GetComponent<Animator>().SetBool("onWall", true);
                    isJumping = false;
                    wallJumping = false;
                    if (Mathf.Abs(Input.GetAxis("Horizontal")) > .7)
                    {
                        playerBody.velocity = new Vector2(0, 0);
                    }
                    if (Mathf.Abs(Input.GetAxis("Horizontal")) < .7)
                    {
                        playerBody.velocity = new Vector2(playerBody.velocity.x, -2);
                    }
                }
            }
            else if (!touchingWall)
            {
                GetComponent<Animator>().SetBool("onWall", false);
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

            if (isJumping && !recentlyJumped) // counter jump force: if you release W after jumping you don't jump as high. In other words the longer you hold W the higher you jump.
            {
                if (!jumpKeyHeld && Vector2.Dot(playerBody.velocity, Vector2.up) > 0)
                {
                    playerBody.AddForce(counterJumpForce * playerBody.mass * Vector2.down);
                }
            }

            if (wallJumping && !recentlyJumped) // counter jump force but for wall jumps. If you release W you don't jump as high, if you hold opposite direction of your flight path you don't fly as far
            {

                if (!jumpKeyHeld && Vector2.Dot(playerBody.velocity, Vector2.up) > 0)
                {
                    playerBody.AddForce(counterJumpForce/Mathf.Sqrt(2) * playerBody.mass * -Vector2.up);
                }
                if (Mathf.Sign(playerBody.velocity.x) != Mathf.Sign(Input.GetAxis("Horizontal")))
                    {
                    playerBody.AddForce(counterJumpForce / (2 * Mathf.Sqrt(2)) * playerBody.mass * new Vector2(-playerDirection, 0));
                    }
            }
            if (recentlyJumped) // timer creates a minimum jump height with counterjumpforce (without this timer tapping w makes you jump less than one block tall
            {
                jumpFallCooldown -= Time.smoothDeltaTime;

                if (jumpFallCooldown <= 0)
                {
                    recentlyJumped = false;
                }
                else
                {
                    recentlyJumped = true;
                }
            }

            if (isFloating)
            {
                floatingTimer -= Time.smoothDeltaTime;

                if (floatingTimer <= 0)
                {
                    isFloating = false;
                    playerBody.gravityScale = playerGravity;
                }
                else
                {
                    isFloating = true;
                    playerBody.gravityScale = 0;
                }
            }

            if (JumpDetector.OnGround)
            {
                isFloating = false;
                floatingTimer = .1f;
            }
            else if (!(JumpDetector.OnGround || isJumping || touchingWall || wallJumping) && floatingTimer > 0)
            {
                isFloating = true;
            }
        }
        Debug.Log("On Ground: " + JumpDetector.OnGround.ToString() + " Touching Wall: " + touchingWall.ToString());
    }

    public void Jump()
    {
        jumpFallCooldown = .1f;
        playerBody.AddForce(Vector2.up * jumpForce * 180);
        PlayerAnim.SetTrigger("Jump");
        isJumping = true;
        wallJumping = false;
        recentlyJumped = true;
    }

    public void WallJump()
    {
        jumpFallCooldown = .25f;
        transform.localPosition = transform.position + new Vector3(.1f * playerDirection, 0, 0);
        playerBody.AddForce(walljumpVector / Mathf.Sqrt(2) * jumpForce * 180);
        wallJumping = true;
        isJumping = false;
        recentlyJumped = true;
        GetComponent<Animator>().SetBool("onWall", false);
        FlipCharacter();
    }

    public void WallRaycasting()
    {
        Debug.DrawLine(transform.position, wallEndLine.position, Color.red);  // during playtime, projects a line from a start point to and end point
        touchingWall = Physics2D.Linecast(transform.position, wallEndLine.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    public void FlipCharacter()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    private void Update()
    {
        if (PlayerHealth.Dead == false) // Only allow inputs if alive
        {
            //// JUMPING
            if (Input.GetKeyDown(KeyCode.W))
            {
                jumpKeyHeld = true;

                if (!JumpDetector.OnGround && touchingWall && Mathf.Abs(Input.GetAxis("Horizontal")) < .7 ) //Walljump
                { 
                    WallJump();
                }

                if (!JumpDetector.OnGround && jumpCount > 0) //Double jump
                {
                    playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
                    Jump();
                    jumpCount -= 1;
                }

                if (JumpDetector.OnGround && !recentlyJumped) // Checks to see if player is on ground before jumping
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
            }

            if (playerBody.velocity.x < -.5f)
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
        }
    }
}

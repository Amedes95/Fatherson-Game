using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D playerBody;
    public float playerSpeed;
    public float jumpForce;
    public float counterJumpForce;
    private float fallVelocity;
    private Animator PlayerAnim;
    public float jumpHeight;
    public static bool isJumping;
    bool jumpKeyHeld;
    public static int jumpCount;

    public Transform PlayerSpawn; // passed in through editor

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        PlayerAnim = GetComponent<Animator>();
        transform.position = PlayerSpawn.position; // spawn

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

            if (Mathf.Abs(playerBody.velocity.x) > 7 && JumpDetector.OnGround) //ground speed cap
            {
                playerSpeed = 16f;
            }
            else if (Mathf.Abs(playerBody.velocity.x) < 3 && JumpDetector.OnGround) //quick movement from rest
            {
                playerSpeed = 60;
            }
            else if (JumpDetector.OnGround)
            {
                playerSpeed = 18;
            }

            if (Mathf.Abs(playerBody.velocity.x) > 10 && !JumpDetector.OnGround) //air speed cap
            {
                playerBody.AddForce(movementPlayer * playerSpeed * -1);
            }


            if (fallVelocity < -10)
            {
                playerBody.AddForce(Vector2.up * playerBody.gravityScale * 10);
            }

            ///// RUNNING
            if (moveHorizontal > 0f) // player is moving right
            {
                playerBody.AddForce(movementPlayer * playerSpeed);
                PlayerAnim.SetBool("Running", true);
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            if (moveHorizontal < 0f) // player is moving left
            {
                playerBody.AddForce(movementPlayer * playerSpeed);
                PlayerAnim.SetBool("Running", true);
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            if (Mathf.Abs(moveHorizontal) < .01) // player is stopped
            {
                PlayerAnim.SetBool("Running", false);
            }

            if (isJumping)
            {
                if (!jumpKeyHeld && Vector2.Dot(playerBody.velocity, Vector2.up) > - 0)
                {
                    playerBody.AddForce(counterJumpForce * playerBody.mass * Vector2.down);
                }
            }
        }
        //Debug.Log(playerBody.velocity.x);
    }

    public void Jump()
    {
        Vector2 jump = new Vector2(0, 180);
        playerBody.AddForce(jump * jumpForce);
        PlayerAnim.SetTrigger("Jump");
        isJumping = true;
    }

    private void Update()
    {
        if (PlayerHealth.Dead == false) // Only allow inputs if alive
        {
            //// JUMPING
            if (Input.GetKeyDown(KeyCode.W))
            {
                jumpKeyHeld = true;

                if (!JumpDetector.OnGround && jumpCount > 0)
                {
                    playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
                    Jump();
                    jumpCount -= 1;
                }

                if (JumpDetector.OnGround) // Checks to see if player is on ground before jumping
                {
                    Jump();
                    //isJumping = true;
                }
                
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                jumpKeyHeld = false;
            }
        }
        Debug.Log(isJumping);
    }

    public void Respawn()
    {
        //moves the player to SpawnPosition
        transform.position = PlayerSpawn.position;
    }
}

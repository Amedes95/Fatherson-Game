using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D playerBody;
    public float playerSpeed;
    public static float moveHorizontal; // used for horizontal movement controls
    public static float jumpForce;
    public float counterJumpForce; // used to make the player descend if they release w while jumping
    private Animator PlayerAnim;
    public float jumpHeight;
    public static bool isJumping;
    public static bool wallJumping;
    public static float jumpFallCooldown; // a timer used to make a minimum jump height with counterJumpForce
    public static bool recentlyJumped;
    bool jumpKeyHeld;
    public static int jumpCount; // used for double jumps, not currently implemented
    public float fallForce;
    public float fallSpeedCap;
    public float riseSpeedCap;
    public float startSpeed;
    public float midSpeed;
    public float fullSpeed;
    public static float maxVelocity;
    public float midVelocity;
    public Transform WallClingStart;
    public Transform wallEndLine;
    bool touchingWall; // used for wall slide animation and walljump
    public Transform backWallEndLine;
    bool backTouchingWall;
    public Transform floatLine;
    int playerDirection;
    Vector2 walljumpVector;
    public static bool isFloating;
    public static float floatingTimer; // used for the "coyote effect", the player doesn't fall for a brief moment after running off a ledge and can jump during this time
    public float playerGravity;
    public Animator WindEffect;
    public float wallJumpBuffer; // used to make the player remain on the wall for the duration while holding away from the wall
    float setWallJumpBuffer;
    public bool flipOnSpawn;
    public static Vector2 playerVelocity;
    private float jumpBuffer; // used to buffer a jump if jump is inputted before hitting the ground

    public PlayerSoundScript jumpAudioBox;


    //////// Stuff used here is for particle systems
    public ParticleSystem DustKickup;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        PlayerAnim = GetComponent<Animator>();
        startSpeed = 200;
        midSpeed = 26;
        fullSpeed = 20;
        maxVelocity = 8;
        midVelocity = 5;
        floatingTimer = -1;
        playerGravity = 2;
        setWallJumpBuffer = wallJumpBuffer;
        jumpBuffer = -1;
        jumpForce = CalculateJumpForce(playerBody.gravityScale, jumpHeight);

        if (flipOnSpawn)
        {
            FlipCharacter();
        }
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
            moveHorizontal = Input.GetAxis("Horizontal"); // left is -1, stopped is 0, right is 1
            Vector2 movementPlayer = new Vector2(moveHorizontal, 0);
            playerVelocity = playerBody.velocity;

            if (transform.localScale.x > 0)
            { playerDirection = 1; }
            else if (transform.localScale.x < 0)
            { playerDirection = -1; }

            walljumpVector = new Vector2(-.8f * playerDirection, 1); // tinker with this

            if (playerBody.velocity.y < -1)
            {
                playerBody.AddForce(new Vector2(0, -1) * fallForce);
            }

            if (touchingWall && !JumpDetector.OnGround) //Wall Slide
            {
                CreateDust();
                fallSpeedCap = 6;
                floatingTimer = 0;
                GetComponent<Animator>().SetBool("onWall", true);
                isJumping = false;

                wallJumpBuffer = setWallJumpBuffer;

                if (Mathf.Abs(moveHorizontal + playerDirection) > 1) //this fixes the bug where the player would stop moving downwards (cling) when holding into the wall
                {
                    playerSpeed = 0;
                }
                else
                { playerSpeed = midSpeed; }

            }
            else if (!touchingWall)
            {
                fallSpeedCap = 15;
                playerSpeed = midSpeed;
                GetComponent<Animator>().SetBool("onWall", false);
                if (wallJumpBuffer > 0)
                {
                    wallJumpBuffer -= Time.smoothDeltaTime;
                }
            }

            if (backTouchingWall && !JumpDetector.OnGround)
            {
                FlipCharacter();
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

            if (playerBody.velocity.y < -fallSpeedCap) //fall speed cap
            {
                playerBody.AddForce(Vector2.up * playerBody.gravityScale * 15);
            }
            //else if (playerBody.velocity.y > riseSpeedCap)
            //{
            //    playerBody.AddForce(Vector2.down * playerBody.gravityScale * 15);
            //}

            ///// Vertical speed limiter
            ///// This should hopefully fix the notorious "superjump" bug that occurs when jumping,
            ///// bonking enemies, jumping on springs, and walljumping off corners

            if ((playerVelocity.y > riseSpeedCap) || (playerVelocity.y < fallSpeedCap))
            {
                playerVelocity = new Vector2(
                    playerVelocity.x,
                    Mathf.Clamp(playerVelocity.y, -fallSpeedCap, riseSpeedCap + .5f)
                    );
            }

            ///// Movement left and right
            if ((moveHorizontal > 0f) && !Trampoline.isBonking) // player is moving right
            {
                if ((Mathf.Sign(moveHorizontal) != Mathf.Sign(playerBody.velocity.x)) && !recentlyJumped) // this makes the character turn around quicker in the air for more control
                {
                    playerBody.AddForce(movementPlayer * playerSpeed * 3);
                }
                else
                {
                    playerBody.AddForce(movementPlayer * playerSpeed);
                }

                PlayerAnim.SetBool("Running", true);
            }
            if ((moveHorizontal < 0f) && !Trampoline.isBonking) // player is moving left
            {
                if ((Mathf.Sign(moveHorizontal) != Mathf.Sign(playerBody.velocity.x)) && !recentlyJumped)
                {
                    playerBody.AddForce(movementPlayer * playerSpeed * 3);
                }
                else
                {
                    playerBody.AddForce(movementPlayer * playerSpeed);
                }
                PlayerAnim.SetBool("Running", true);
            }

            if (Mathf.Abs(moveHorizontal) < .01) // player is stopped
            {
                PlayerAnim.SetBool("Running", false);
                if (!recentlyJumped && !wallJumping)
                {
                    playerBody.AddForce(-playerDirection * playerSpeed * Vector2.right * Mathf.Abs(playerVelocity.x) / 10); //this slows down the player if they arent holding A/D
                }
            }

            if (isJumping && !recentlyJumped) // counter jump force: if you release W after jumping you don't jump as high. In other words the longer you hold W the higher you jump.
            {
                if (!jumpKeyHeld && Vector2.Dot(playerBody.velocity, Vector2.up) > 0)
                {
                    playerBody.AddForce(counterJumpForce * playerBody.mass * Vector2.down);
                }
                else if (Vector2.Dot(playerBody.velocity, Vector2.down) > 0) //this is new, may cause bugs
                {
                    isJumping = false;
                    floatingTimer = -1;
                }
            }

            if (wallJumping && !recentlyJumped) // counter jump force but for wall jumps. If you release W you don't jump as high, if you hold opposite direction of your flight path you don't fly as far
            {

                if (!jumpKeyHeld && Vector2.Dot(playerBody.velocity, Vector2.up) > 0)
                {
                    playerBody.AddForce(counterJumpForce / Mathf.Sqrt(2) * playerBody.mass * -Vector2.up);
                }
                else if (Vector2.Dot(playerBody.velocity, Vector2.down) > 0) //this is new, may cause bugs
                {
                    wallJumping = false;
                    floatingTimer = -1;
                }
            }

            if (recentlyJumped) // timer creates a minimum jump height with counterjumpforce (without this timer tapping w makes you jump less than one block tall
            {
                jumpFallCooldown -= Time.smoothDeltaTime;
                playerSpeed = 0;

                if (jumpFallCooldown <= 0)
                {
                    recentlyJumped = false;
                    playerSpeed = midSpeed;
                }
                else
                {
                    recentlyJumped = true;
                }
            }

            if (isFloating)
            {
                floatingTimer -= Time.smoothDeltaTime;

                if (floatingTimer <= 0 || Mathf.Sign(moveHorizontal) != Mathf.Sign(playerBody.velocity.x))
                {
                    SwitchFloatValue(false);
                }
                else
                {
                    playerBody.AddForce(Vector2.up * playerBody.gravityScale * playerBody.mass * 1.4f);
                    //playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
                }
            }

            if (JumpDetector.OnGround)
            {
                SwitchFloatValue(false);
                floatingTimer = .1f;
            }
            else if
                (!(JumpDetector.OnGround || isJumping || touchingWall || wallJumping || Trampoline.isBonking) && floatingTimer > 0)
            {
                SwitchFloatValue(true);
            }

            if (StickyWeb.StuckInWeb)
            {
               playerBody.velocity = new Vector2 (Mathf.Clamp(playerBody.velocity.x, -2, 2), Mathf.Clamp(playerBody.velocity.y, -2, 8)) ;
            }

            //Below code is a jump buffer when landing on ground
            //If you press w in this time before touching ground you will still jump

            if (!JumpDetector.OnGround && Input.GetKeyDown(KeyCode.W) && !recentlyJumped)
            {
                jumpBuffer = .2f;
            }

            if (jumpBuffer > 0)
            {
                jumpBuffer -= Time.smoothDeltaTime;
            }
            

            if (JumpDetector.OnGround && jumpBuffer > 0)
            {
                Jump();
            }
        }
    }

    public void SwitchFloatValue(bool variable)
    {
        if (variable == true)
        {
            isFloating = true;
            playerBody.gravityScale = 0;
        }
        else if (variable == false)
        {
            isFloating = false;
            playerBody.gravityScale = playerGravity;
        }
    }

    public void Jump()
    {
        if (KeepWithPlatform.OnPlatform) //make this universal for all moving platforms
        {
            playerBody.velocity= new Vector2(playerBody.velocity.x, 0);
        }
        if (Trampoline.isBonking == false)
        {
            jumpFallCooldown = .05f;
            recentlyJumped = true;
            playerBody.AddForce(Vector2.up * jumpForce * 180);
            PlayerAnim.SetTrigger("Jump");
            isJumping = true;
            wallJumping = false;
            CreateDust();
            jumpAudioBox.playJumpSound();
        }


    }

    public void WallJump()
    {
        if (Trampoline.isBonking == false)
        {
            playerBody.velocity = new Vector2(0, 0);
            jumpFallCooldown = .15f;
            recentlyJumped = true;
            playerBody.AddForce(walljumpVector * jumpForce * 180);
            wallJumping = true;
            isJumping = false;
            GetComponent<Animator>().SetBool("onWall", false);
            CreateDust();
            jumpAudioBox.playWallJumpSound();
        }


    }

    public void WallRaycasting()
    {
        Debug.DrawLine(WallClingStart.position, wallEndLine.position, Color.red);  // during playtime, projects a line from a start point to and end point
        touchingWall = Physics2D.Linecast(WallClingStart.position, wallEndLine.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    public void backWallRaycasting()
    {
        Debug.DrawLine(transform.position, backWallEndLine.position, Color.red);  // during playtime, projects a line from a start point to and end point
        backTouchingWall = Physics2D.Linecast(transform.position, backWallEndLine.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    public void FlipCharacter()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        if (JumpDetector.OnGround && !touchingWall)
        {
            CreateDust();
        }
    }

    private void Update()
    {
        if (PlayerHealth.Dead == false) // Only allow inputs if alive
        {
            //// JUMPING
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumpKeyHeld = true;

                if (JumpDetector.OnGround && !recentlyJumped && jumpBuffer < 0) // Checks to see if player is on ground before jumping
                {
                    Jump();
                }
                else if (!JumpDetector.OnGround && (touchingWall || (wallJumpBuffer > 0 && (Mathf.Abs(playerVelocity.x) < 2)))) //Walljump, can only jump if you are not holding into the wall
                {
                    WallJump();
                    jumpCount = 0;
                }
                else if (!JumpDetector.OnGround && jumpCount > 0) //Double jump
                {
                    playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
                    Jump();
                    jumpCount -= 1;
                }
                else if (isFloating)
                {
                    playerBody.velocity = new Vector2(playerBody.velocity.x, 4); // this makes the jump height consistent with grounded jumps. If this is not enabled, floating jumps are about 5/6ths of regular jumps
                    Jump();
                    SwitchFloatValue(false);
                }

            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                jumpKeyHeld = false;
            }

            //// Walljumping
            WallRaycasting();
            backWallRaycasting();

            ////Face direction of horizontal movement
            if (playerBody.velocity.x > 3f)
            {
                if (transform.localScale.x < 0)
                {
                    FlipCharacter();
                }
            }

            if (playerBody.velocity.x < -3f)
            {
                if (transform.localScale.x > 0)
                {
                    FlipCharacter();
                }
            }
        }
    }

    void CreateDust()
    {
        DustKickup.Play();
    }
}

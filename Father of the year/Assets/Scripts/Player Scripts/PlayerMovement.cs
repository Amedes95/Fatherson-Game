using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.InputSystem;


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
    public bool jumpKeyHeld;
    public static int jumpCount; // used for double jumps, used with fruit
    public float fallForce;
    public float fallSpeedCap;
    public float riseSpeedCap;
    public float startSpeed;
    public float normalSpeed;
    public float slowSpeed;
    public static float maxVelocity;
    public float midVelocity;
    public Transform WallClingStart;
    public Transform wallEndLine;
    bool touchingWall; // used for wall slide animation and walljump
    bool touchingIce;
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
    public GameObject DoubleJumpEffect;

    public bool ControllerEnabled;
    string JumpInput;

    public bool FanFloating;

    public static bool JustBounced;
    public static float BounceBuffer = .2f;

    public static bool PlayerInvincible;
    public static float InvincibilityTimer;
    public GameObject LavaShield;
    public GameObject IceBlock;



    //////// Stuff used here is for particle systems
    public ParticleSystem DustKickup;

    void Awake()
    {
        jumpCount = 0;
        playerBody = GetComponent<Rigidbody2D>();
        PlayerAnim = GetComponent<Animator>();
        startSpeed = 200;
        normalSpeed = 26;
        slowSpeed = 20;
        maxVelocity = 8;
        midVelocity = 5;
        floatingTimer = -1;
        playerGravity = 2;
        setWallJumpBuffer = wallJumpBuffer;
        jumpBuffer = -1;
        jumpForce = CalculateJumpForce(playerBody.gravityScale, jumpHeight);
        InvincibilityTimer = 0f;
        wallJumping = false;
        isFloating = false;
        JumpDetector.OnGround = false;



        if (flipOnSpawn)
        {
            FlipCharacter();
        }
    }

    public static float CalculateJumpForce(float playerGravity, float jumpHeight)
    {
        return Mathf.Sqrt(2 * playerGravity * jumpHeight);
    }

    public void SpawnDoubleJumpParticles()
    {
        DoubleJumpEffect.GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Detect the method of input we should be referencing
        if (Boombox.ControllerModeEnabled)
        {
            if (Boombox.PS4Enabled) // ps4
            {
                if (Application.platform != (RuntimePlatform.LinuxPlayer) && Application.platform != (RuntimePlatform.LinuxEditor)) // don't change controls for Linux drivers
                {
                    JumpInput = "PS4Jump";
                }
                else
                {
                    JumpInput = "JumpController";
                }
            }
            else // everything but ps4
            {
                //Debug.Log("Xbox jump");
                JumpInput = "JumpController";
            }
        }
        else // no controller, use keyboard
        {
            JumpInput = "Jump";
        }
        if (BounceBuffer > 0)
        {
            BounceBuffer -= Time.deltaTime;
        }
        else if (BounceBuffer <= 0)
        {
            BounceBuffer = 0;
        }

        // toggles frozen ice block when player is frozen
        if (FreezePlayer.Frozen)
        {
            IceBlock.SetActive(true);
            floatingTimer = 0;
            isFloating = false;
        }
        else
        {
            IceBlock.SetActive(false);
        }

        if (PlayerHealth.Dead == false && FreezePlayer.Frozen == false) // Only allow movement if alive and not frozen
        {


            //// Walljumping
            WallRaycasting();
            backWallRaycasting();
            IceRaycasting();
            if (jumpCount > 0 && JumpDetector.OnGround == false)
            {
                gameObject.GetComponent<Animator>().SetBool("DoubleJumpActive", true);
                Boombox.SetVibrationIntensity(.1f, .15f, .15f); // vibrate a lil bit ;)

            }
            else
            {
                DoubleJumpEffect.GetComponent<ParticleSystem>().Stop();
                gameObject.GetComponent<Animator>().SetBool("DoubleJumpActive", false);
            }

            ///// Lava invincibility timer
            if (InvincibilityTimer > 0)
            {
                //Debug.Log(InvincibilityTimer);
                InvincibilityTimer -= Time.smoothDeltaTime;
                PlayerInvincible = true; // Invincible while timer is active
                LavaShield.GetComponent<SpriteRenderer>().enabled = true;
                LavaShield.GetComponent<Animator>().SetBool("Active", true);

                if (InvincibilityTimer <= 3.5f) // warning effect for lava shield
                {
                    LavaShield.GetComponent<Animator>().SetBool("Warning", true);
                }
                else
                {
                    LavaShield.GetComponent<Animator>().SetBool("Warning", false);
                }
            }

            else if (InvincibilityTimer <= 0)
            {
                InvincibilityTimer = 0f;
                PlayerInvincible = false;
                LavaShield.GetComponent<SpriteRenderer>().enabled = false;
                LavaShield.GetComponent<Animator>().SetBool("Active", false);
                LavaShield.GetComponent<Animator>().SetBool("Warning", false);

            }



            //// set the axis of input for the player
            if (Boombox.ControllerModeEnabled == false)
            {
                moveHorizontal = Input.GetAxis("Horizontal"); // left is -1, stopped is 0, right is 1
            }
            else
            {
                moveHorizontal = Input.GetAxis("ControllerHorizontal"); // left is -1, stopped is 0, right is 1
            }
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
                gameObject.GetComponent<Animator>().SetBool("Falling", true);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("Falling", false);
            }

            if (touchingWall && !JumpDetector.OnGround) //Wall Slide
            {
                Boombox.SetVibrationIntensity(.1f, .15f, .15f); // vibrate a lil bit ;)
                jumpCount = 0;
                CreateDust();
                if (touchingIce) // NEW 5/22/20 ICE WALLSLIDE
                {
                    fallSpeedCap = 12;
                }
                else
                {
                    fallSpeedCap = 6;
                }
                floatingTimer = 0;
                GetComponent<Animator>().SetBool("onWall", true);
                wallJumpBuffer = setWallJumpBuffer;

                if (Mathf.Abs(moveHorizontal + playerDirection) > 1) //this fixes the bug where the player would stop moving downwards (cling) when holding into the wall
                {
                    playerSpeed = 0;
                }
                else
                { playerSpeed = normalSpeed; }

            }
            else if (!touchingWall)
            {
                fallSpeedCap = 15;
                playerSpeed = normalSpeed;
                GetComponent<Animator>().SetBool("onWall", false);
                if (wallJumpBuffer > 0)
                {
                    wallJumpBuffer -= Time.smoothDeltaTime;
                }
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            }

            //// Ths fixes the bug where you don't stick to a walljump if you jump up to it facing backwards
            if (backTouchingWall && !JumpDetector.OnGround)
            {
                FlipCharacter();
            }

            if (Mathf.Abs(playerBody.velocity.x) > maxVelocity && JumpDetector.OnGround) //ground speed cap
            {
                playerSpeed = slowSpeed;
            }
            else if (JumpDetector.OnGround && IceZone.OnIce == false) // This makes the player turn sharper (ICE STUFF ADDED RECENTLY 5/22/20)
            {
                playerSpeed = normalSpeed;
                if (moveHorizontal == 0f && FanFloating == false && IceZone.OnIce == false)
                {
                    playerBody.velocity = new Vector2(0, playerBody.velocity.y);
                }
                if (moveHorizontal > 0f && playerBody.velocity.x < 0f) // player is grounded, moving right, and pressing left input
                {
                    playerBody.velocity = new Vector2(0, playerBody.velocity.y);
                }
                else if (moveHorizontal < 0f && playerBody.velocity.x > 0f) // player is grounded, moving left, and pressing right input
                {
                    playerBody.velocity = new Vector2(0, playerBody.velocity.y);
                }
            }
            else if (Mathf.Abs(playerBody.velocity.x) < midVelocity && JumpDetector.OnGround && IceZone.OnIce == false) //quick burst of movement from rest
            {
                playerSpeed = startSpeed;
            }
            else if (Mathf.Abs(playerBody.velocity.x) < midVelocity && JumpDetector.OnGround && IceZone.OnIce == true && touchingWall) //quick burst of movement from rest
            {
                playerSpeed = normalSpeed;
            }

            //// Air speed cap
            if (Mathf.Abs(playerBody.velocity.x) > maxVelocity && !JumpDetector.OnGround)
            {
                playerBody.AddForce(movementPlayer * playerSpeed * -1);
            }

           ///// Fall speed cap
            if (playerBody.velocity.y < -fallSpeedCap)
            {
                playerBody.AddForce(Vector2.up * playerBody.gravityScale * 15);
            }
            if (playerBody.velocity.y > riseSpeedCap)
            {
                playerBody.AddForce(Vector2.down * playerBody.gravityScale * 15);
            }

            ///// Rise and fall velocity clamps
            if (((playerVelocity.y > riseSpeedCap)))
            {
                Debug.Log(playerVelocity.y);
                playerVelocity = new Vector2(playerVelocity.x, riseSpeedCap);
                //playerVelocity = new Vector2(playerVelocity.x, Mathf.Clamp(playerVelocity.y, -fallSpeedCap, riseSpeedCap));
            }
            else if ((playerVelocity.y < fallSpeedCap))
            {
                playerVelocity = new Vector2(playerVelocity.x, -fallSpeedCap);
            }

            ///// Movement left and right
            if ((moveHorizontal > 0f) && !Trampoline.IsBouncing && BounceBuffer <= 0) // player is moving right
            {
                if ((moveHorizontal > 0f && playerBody.velocity.x < 0f) && !recentlyJumped && (Mathf.Abs(playerVelocity.x) > 2) && JumpDetector.OnGround == false && KeepWithPlatform.OnPlatform == false) // this makes the character turn around quicker in the air for more control, I add the >2 part to prevent backdashing upond landing on the ground
                {
                    playerBody.velocity = new Vector2(0, playerBody.velocity.y);
                    playerBody.AddForce(movementPlayer * playerSpeed * 3);
                }
                else
                {
                    playerBody.AddForce(movementPlayer * playerSpeed);
                }
                PlayerAnim.SetBool("Running", true);
            }
            if ((moveHorizontal < 0f) && !Trampoline.IsBouncing && BounceBuffer <= 0) // player is moving left
            {
                if ((moveHorizontal < 0f && playerBody.velocity.x > 0f) && !recentlyJumped && (Mathf.Abs(playerVelocity.x) > 2) && JumpDetector.OnGround == false && KeepWithPlatform.OnPlatform == false)
                {
                    playerBody.velocity = new Vector2(0, playerBody.velocity.y);
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
            }

            if (isJumping && !recentlyJumped && FanFloating == false && JustBounced == false && touchingIce == false) // counter jump force: if you release W after jumping you don't jump as high. In other words the longer you hold W the higher you jump.
            {

                if (Gamepad.current != null)
                {
                    Gamepad.current.PauseHaptics();
                }

                if (!jumpKeyHeld && Vector2.Dot(playerBody.velocity, Vector2.up) > 0)
                {
                    if (FanFloating == false) // This is new // 3/1/20
                    {
                        playerBody.AddForce(counterJumpForce * playerBody.mass * Vector2.down);
                    }
                }
                else if (Vector2.Dot(playerBody.velocity, Vector2.down) > 0)
                {
                    isJumping = false;
                    floatingTimer = -1;
                }
            }

            if (wallJumping && !recentlyJumped && FanFloating == false && JustBounced == false) // counter jump force but for wall jumps. If you release W you don't jump as high, if you hold opposite direction of your flight path you don't fly as far
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
                floatingTimer = 0;
                isFloating = false;

                if (jumpFallCooldown <= 0)
                {
                    recentlyJumped = false;
                    playerSpeed = normalSpeed;
                }
                else
                {
                    recentlyJumped = true;
                }
            }

            if (isFloating)
            {
                //Debug.Log("Floating");
                floatingTimer -= Time.smoothDeltaTime;
                playerBody.gravityScale = 0;
                if (floatingTimer <= 0 || Mathf.Sign(moveHorizontal) != Mathf.Sign(playerBody.velocity.x))
                {
                    SwitchFloatValue(false);
                }
            }
            else
            {
                playerBody.gravityScale = 2;
            }

            if (JumpDetector.OnGround)
            {
                //SwitchFloatValue(false);
                wallJumpBuffer = 0;
                JustBounced = false;
                floatingTimer = .1f;
                isFloating = false;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else if (!(JumpDetector.OnGround || isJumping || touchingWall || wallJumping || Trampoline.IsBouncing) && floatingTimer > 0)
            {
                SwitchFloatValue(true);
            }

            if (StickyWeb.StuckInWeb)
            {
               playerBody.velocity = new Vector2 (Mathf.Clamp(playerBody.velocity.x, -2, 2), Mathf.Clamp(playerBody.velocity.y, -2, 8)) ;
            }
            if (touchingWall)
            {
                JustBounced = false;
            }
            if (Trampoline.IsBouncing)
            {
                JustBounced = true;
            }
            if (jumpCount > 0)
            {
                JustBounced = false;
            }


            //Below code is a jump buffer when landing on ground
            //If you press w in this time before touching ground you will still jump

            if (!JumpDetector.OnGround && Input.GetButtonDown(JumpInput) && !recentlyJumped)
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

        ////Face direction of horizontal movement
        if (playerBody.velocity.x > 1f)
        {
            if (transform.localScale.x < 0)
            {
                FlipCharacter();
            }
        }

        if (playerBody.velocity.x < -1f)
        {
            if (transform.localScale.x > 0)
            {
                FlipCharacter();
            }
        }
    }

    public void SwitchFloatValue(bool variable)
    {
        if (variable == true)
        {
            isFloating = true;
            //playerBody.gravityScale = 0;
        }
        else if (variable == false)
        {
            isFloating = false;
            //playerBody.gravityScale = playerGravity;
        }
    }

    public void Jump()
    {
        playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerBody.gravityScale = 2;

        jumpForce = CalculateJumpForce(playerBody.gravityScale, jumpHeight);
        if (KeepWithPlatform.OnPlatform) //make this universal for all moving platforms
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
        }
        if (Trampoline.IsBouncing == false) // and not bonking? 
        {
            if (!wallJumping && (JumpDetector.OnGround == true || isFloating) || (!wallJumping && jumpCount > 0)) /////// this check is new 4/8/20 to fix two-way platform launching
            {
                isFloating = false;
                floatingTimer = 0;
                jumpFallCooldown = .05f;
                recentlyJumped = true;
                if (touchingIce)
                {
                    playerBody.AddForce(Vector2.up * jumpForce * 210);
                }
                else
                {
                    playerBody.AddForce(Vector2.up * jumpForce * 180);
                }
                PlayerAnim.SetTrigger("Jump");
                isJumping = true;
                wallJumping = false;
                CreateDust();
                jumpAudioBox.playJumpSound();
            }

        }
        Boombox.SetVibrationIntensity(.1f, .25f, .75f); // vibrate a lil bit ;)
        Boombox.SetVibrationIntensity(.1f, .25f, .75f); // vibrate a lil bit ;)
    }

    public void WallJump()
    {
        if (Trampoline.IsBouncing == false)
        {
            playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;

            jumpForce = CalculateJumpForce(playerBody.gravityScale, jumpHeight);
            playerBody.velocity = new Vector2(0, 0);
            jumpFallCooldown = .15f;
            recentlyJumped = true;
            playerBody.AddForce(walljumpVector * jumpForce * 180);
            wallJumping = true;
            isJumping = false;
            GetComponent<Animator>().SetBool("onWall", false);
            CreateDust();
            jumpAudioBox.playWallJumpSound();
            Boombox.SetVibrationIntensity(.3f, .08f, .08f); // vibrate a lil bit ;)

        }


    }

    public void WallRaycasting()
    {
        Debug.DrawLine(WallClingStart.position, wallEndLine.position, Color.red);  // during playtime, projects a line from a start point to and end point
        touchingWall = Physics2D.Linecast(WallClingStart.position, wallEndLine.position, 1 << LayerMask.NameToLayer("Ground"));
    }


    public void IceRaycasting()
    {
        Debug.DrawLine(WallClingStart.position, wallEndLine.position, Color.red);  // during playtime, projects a line from a start point to and end point
        touchingIce = Physics2D.Linecast(WallClingStart.position, wallEndLine.position, 1 << LayerMask.NameToLayer("Ice"));
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
        // Makes the player pivot sharper
        if (!wallJumping && JumpDetector.OnGround)
        {
            playerBody.velocity = new Vector2(0, playerBody.velocity.y);
        }
    }

    private void Update()
    {
        // Detect the method of input we should be referencing
        if (Boombox.ControllerModeEnabled)
        {
            if (Boombox.PS4Enabled) // ps4
            {
                if (Application.platform != (RuntimePlatform.LinuxPlayer) && Application.platform != (RuntimePlatform.LinuxEditor)) // don't change controls for Linux drivers
                {
                    JumpInput = "PS4Jump";
                }
                else
                {
                    JumpInput = "JumpController";
                }
            }
            else // everything but ps4
            {
                JumpInput = "JumpController";
            }
        }
        else
        {
            JumpInput = "Jump";
        }
        if (jumpCount > 0)
        {
            gameObject.GetComponent<Animator>().SetBool("DoubleJumpActive", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("DoubleJumpActive", false);
        }

        if (PlayerHealth.Dead == false && PauseMenu.GameIsPaused == false && FreezePlayer.Frozen == false) // Only allow inputs if alive
        {
            //// JUMPING
            if (Input.GetButtonDown(JumpInput))
            {

                jumpKeyHeld = true;

                if (JumpDetector.OnGround && !recentlyJumped && jumpBuffer < 0 && jumpCount == 0) // Checks to see if player is on ground before jumping
                {
                    Jump();
                }
                else if (!JumpDetector.OnGround && touchingWall) //Walljump, can only jump if you are not holding into the wall
                {
                    WallJump();
                    jumpCount = 0;
                }
                else if (!JumpDetector.OnGround && jumpCount > 0) //Double jump
                {
                    Debug.Log("Double Jump!");
                    playerBody.velocity = new Vector2(playerBody.velocity.x, 0);
                    wallJumping = false;
                    Jump();
                    jumpCount -= 1;
                    jumpAudioBox.PlayDoubleJumpSound();

                }
                if (isFloating && jumpCount == 0 && JumpDetector.OnGround == false)
                {
                    Jump();
                }
                isFloating = false;
                floatingTimer = 0;
            }
            else if (Input.GetButtonUp(JumpInput))
            {
                jumpKeyHeld = false;
            }


        }
        if (FreezePlayer.Frozen)
        {
            PlayerAnim.SetBool("Grounded", false);
        }
    }

    void CreateDust()
    {
        DustKickup.Play();
        Boombox.SetVibrationIntensity(.1f, .2f, .2f); // vibrate a lil bit ;)
    }

    private void OnDisable()
    {
        isJumping = false;
        recentlyJumped = false;
        wallJumping = false;
        StickyWeb.StuckInWeb = false;
    }


}

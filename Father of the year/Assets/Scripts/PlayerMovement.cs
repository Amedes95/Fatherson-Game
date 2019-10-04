using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D playerBody;
    public float playerSpeed;
    public float jumpSpeed;
    private float fallVelocity;
    private Animator PlayerAnim;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        PlayerAnim = GetComponent<Animator>();

}

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // left is -1, stopped is 0, right is 1
        fallVelocity = playerBody.velocity.y;

        Vector2 movementPlayer = new Vector2(moveHorizontal, 0);

        if (Mathf.Abs(playerBody.velocity.x) > 10)
        {
            playerSpeed = 20.75f;
        }
        else if (Mathf.Abs(playerBody.velocity.x) < 3)
        {
            playerSpeed = 40;
        }
        else
        {
            playerSpeed = 21.5f;
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

        Debug.Log(playerBody.velocity.x);
    }

    public void Jump()
    {
        Vector2 jump = new Vector2(0, 50);

        playerBody.AddForce(jump * jumpSpeed);
        PlayerAnim.SetTrigger("Jump");
    }

    private void Update()
    {
        //// JUMPING
        if (Input.GetKeyDown(KeyCode.W) && JumpDetector.OnGround) // checks to see if player is on ground
        {
            Jump();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D playerBody;
    public float playerSpeed;
    public float jumpSpeed;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // left is -1, stopped is 0, right is 1

        Vector2 movementPlayer = new Vector2(moveHorizontal, 0);
        Vector2 jump = new Vector2(0, 50);

        if (moveHorizontal > 0f)
        {
            playerBody.AddForce(movementPlayer * playerSpeed);
        }
        if (moveHorizontal < 0f)
        {
            playerBody.AddForce(movementPlayer * playerSpeed);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerBody.AddForce(jump*jumpSpeed);
        }
        Debug.Log(moveHorizontal);
    }

}

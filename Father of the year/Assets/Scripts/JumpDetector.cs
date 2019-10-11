using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetector : MonoBehaviour
{

    public static bool OnGround;
    public Transform EndLine;
    public Transform EndLine2;
    bool grounded2;



    private void Update()
    {
        Raycasting();
        if (grounded2)
        {
            OnGround = true;
        }
        if (OnGround)
        {
            grounded2 = true;
        }

        if (OnGround)
        {
            PlayerMovement.isJumping = false;
            PlayerMovement.jumpCount = 1;
            gameObject.GetComponent<Animator>().SetBool("Grounded", true);
        }
        else
        {
            OnGround = false;
            gameObject.GetComponent<Animator>().SetBool("Grounded", false);
        }
    }


    void Raycasting()
    {
        Debug.DrawLine(transform.position, EndLine.position, Color.green);
        Debug.DrawLine(transform.position, EndLine2.position, Color.green);

        OnGround = Physics2D.Linecast(transform.position, EndLine.position, 1 << LayerMask.NameToLayer("Ground"));
        grounded2 = Physics2D.Linecast(transform.position, EndLine2.position, 1 << LayerMask.NameToLayer("Ground"));
    }
}

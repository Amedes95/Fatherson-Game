using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetector : MonoBehaviour
{

    public static bool OnGround;
    public Transform EndLine;


    private void Update()
    {
        Raycasting();
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
        OnGround = Physics2D.Linecast(transform.position, EndLine.position, 1 << LayerMask.NameToLayer("JumpZone"));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetector : MonoBehaviour
{

    public static bool OnGround;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            if (!PlayerHealth.Dead)
            {
                OnGround = true;
                PlayerMovement.isJumping = false;
                PlayerMovement.wallJumping = false;
                PlayerMovement.jumpCount = 0;
                collision.GetComponentInParent<Animator>().SetBool("Grounded", true);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            if (!PlayerHealth.Dead)
            {
                if (collision != null)
                {
                    OnGround = false;
                    collision.GetComponentInParent<Animator>().SetBool("Grounded", false);
                }

            }

        }
    }
}

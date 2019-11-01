using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetector : MonoBehaviour
{
    public static bool OnGround;
    GameObject Player;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            collision.GetComponentInParent<PlayerMovement>().audioBox.playLandingSound();
        }
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
                if (collision)
                {
                    OnGround = false;
                    if (Player.activeInHierarchy)
                    {
                        collision.GetComponentInParent<Animator>().SetBool("Grounded", false);
                    }
                }

            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetector : MonoBehaviour
{
    //public int jumpCount; // to be incorporated later (e.g. tile where you have 3 air jumps instead of 1)

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
        if (collision.tag == "Player")
        {
            OnGround = true;
            PlayerMovement.isJumping = false;
            PlayerMovement.jumpCount = 1;
            collision.GetComponent<Animator>().SetBool("Grounded", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnGround = false;
            collision.GetComponent<Animator>().SetBool("Grounded", false);
        }
    }
}

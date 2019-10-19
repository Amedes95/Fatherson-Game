using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkableHead : MonoBehaviour
{
    Vector2 BonkForce;
    public float BounceHeight; // 100 is pretty good
    public float BonkBounceSpeed; // this should match jumpForce in PlayerMovement for consistency
    bool isBonking;


    public void Awake()
    {
        BonkForce = new Vector2(0, BounceHeight); // make this match player jump force, apply if (isJumping) script
        //playerBody = GetComponentInParent<Rigidbody2D>(); // trying to destructure
        isBonking = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {

            if (!PlayerHealth.Dead && !isBonking)
            {
                BonkForce = new Vector2(0, BounceHeight);
                BonkBounceSpeed = PlayerMovement.jumpForce;
                collision.GetComponentInParent<Rigidbody2D>().velocity =
                    new Vector2(collision.GetComponentInParent<Rigidbody2D>().velocity.x, 0);
                collision.GetComponentInParent<Rigidbody2D>().AddForce(BonkForce * BonkBounceSpeed);
                collision.GetComponentInParent<PlayerMovement>().Jump();
                isBonking = true;
                Debug.Log("Boune");
                //collision.GetComponentInParent<Rigidbody2D>().AddForce(Vector2.up * PlayerMovement.jumpForce * 180);
                if (gameObject.tag == "Trampoline")
                {
                    GetComponent<Animator>().SetTrigger("Bounce");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            isBonking = false;
        }
    }
}

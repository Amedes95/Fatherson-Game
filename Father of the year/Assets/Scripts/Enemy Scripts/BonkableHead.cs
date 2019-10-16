using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkableHead : MonoBehaviour
{
    Vector2 BonkForce;
    public float BounceHeight; // 100 is pretty good
    public float BonkBounceSpeed; // this should match jumpForce in PlayerMovement for consistency


    public void Awake()
    {
        BonkForce = new Vector2(0, BounceHeight); // make this match player jump force, apply if (isJumping) script
        //playerBody = GetComponentInParent<Rigidbody2D>(); // trying to destructure

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            if (!PlayerHealth.Dead)
            {
                BonkForce = new Vector2(0, BounceHeight);
                BonkBounceSpeed = PlayerMovement.jumpForce;
                collision.GetComponentInParent<Rigidbody2D>().velocity =
                    new Vector2(collision.GetComponentInParent<Rigidbody2D>().velocity.x, 0);
                collision.GetComponentInParent<Rigidbody2D>().AddForce(BonkForce * BonkBounceSpeed);
                collision.GetComponentInParent<PlayerMovement>().Jump();
                //collision.GetComponentInParent<Rigidbody2D>().AddForce(Vector2.up * PlayerMovement.jumpForce * 180);
                if (gameObject.tag == "Trampoline")
                {
                    GetComponent<Animator>().SetTrigger("Bounce");
                }
            }
        }
    }

}

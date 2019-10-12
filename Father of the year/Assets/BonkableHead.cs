using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkableHead : MonoBehaviour
{
    Vector2 BonkForce;
    public float BounceHeight; // 100 is pretty good
    public float BonkBounceSpeed;


    public void Awake()
    {
        BonkForce = new Vector2(0, BounceHeight);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            if (!PlayerHealth.Dead)
            {
                Debug.Log("Bonk");
                PlayerMovement.jumpCount = 1;
                collision.GetComponentInParent<Rigidbody2D>().AddForce(BonkForce * BonkBounceSpeed);
            }
        }
    }

}

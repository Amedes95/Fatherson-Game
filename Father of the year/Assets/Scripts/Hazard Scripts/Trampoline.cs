using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    float BonkForce; // this should match jumpForce in PlayerMovement for consistency
    bool isBonking;
    public float rotation; //only use this on springs, not enemies
    Vector2 rotationVector;


    public void Awake()
    {
        isBonking = false;
        gameObject.transform.eulerAngles = new Vector3(0, 0, rotation);
        rotationVector = new Vector2(Mathf.Cos(Mathf.PI/180 * (rotation + 90)), Mathf.Sin(Mathf.PI / 180 * (rotation + 90)));
        Debug.Log(rotationVector);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {

            if (!PlayerHealth.Dead && !isBonking)
            {
                BonkForce = PlayerMovement.jumpForce;

                collision.GetComponentInParent<Rigidbody2D>().velocity = rotationVector;
                collision.GetComponentInParent<Rigidbody2D>().AddForce(rotationVector * BonkForce * 180);
                isBonking = true;
                //collision.GetComponentInParent<Rigidbody2D>().AddForce(Vector2.up * PlayerMovement.jumpForce * 180);
                GetComponent<Animator>().SetTrigger("Bounce");
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


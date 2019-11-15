using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    float BonkForce; // this should match jumpForce in PlayerMovement for consistency
    public static bool isBonking;
    public float rotation; // this should match the inputted value for transform.rotation.z
    Vector2 rotationVector;
    public bool sliding;

    public void Awake()
    {
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        gameObject.transform.eulerAngles = new Vector3 (0, 0, rotation);
        isBonking = false;
        rotationVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (rotation + 90)), Mathf.Sin(Mathf.Deg2Rad * (rotation + 90)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {

            if (!PlayerHealth.Dead && !isBonking)
            {
                collision.GetComponentInParent<PlayerMovement>().jumpAudioBox.playJumpSound();
                collision.GetComponentInParent<PlayerMovement>().playerSpeed = 0;

                BonkForce = PlayerMovement.jumpForce;

                collision.GetComponentInParent<Rigidbody2D>().position += .2f * rotationVector;
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
            collision.GetComponentInParent<PlayerMovement>().playerSpeed = collision.GetComponentInParent<PlayerMovement>().midSpeed;
        }
    }
}


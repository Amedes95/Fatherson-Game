using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    float BounceForce; // this should match jumpForce in PlayerMovement for consistency
    public static bool IsBouncing;
    public float rotation; // this should match the inputted value for transform.rotation.z
    Vector2 rotationVector;
    public bool sliding;
    bool EnemyInTrigger;

    public void Awake()
    {
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        gameObject.transform.eulerAngles = new Vector3 (0, 0, rotation);
        IsBouncing = false;
        rotationVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (rotation + 90)), Mathf.Sin(Mathf.Deg2Rad * (rotation + 90)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {

            if (!PlayerHealth.Dead && !IsBouncing)
            {
                PlayerMovement.jumpCount = 0;
                collision.GetComponentInParent<PlayerMovement>().playerSpeed = 0;
                collision.GetComponentInParent<PlayerMovement>().wallJumpBuffer = 0;

                BounceForce = PlayerMovement.CalculateJumpForce(collision.GetComponentInParent<Rigidbody2D>().gravityScale, collision.GetComponentInParent<PlayerMovement>().jumpHeight);

                collision.GetComponentInParent<Rigidbody2D>().position += .2f * rotationVector;
                collision.GetComponentInParent<Rigidbody2D>().velocity = rotationVector;
                if (JumpDetector.OnGround)
                {
                    collision.GetComponentInParent<Rigidbody2D>().AddForce(rotationVector * BounceForce * 180);
                }
                else
                {
                    collision.GetComponentInParent<Rigidbody2D>().AddForce(rotationVector * BounceForce * 180);
                }
                IsBouncing = true;
                PlayerMovement.floatingTimer = -1;
                GetComponent<Animator>().SetTrigger("Bounce");
                collision.GetComponentInParent<Animator>().SetBool("DoubleJumpActive", false);
                collision.GetComponentInParent<Animator>().SetTrigger("Jump");
                Boombox.SetVibrationIntensity(.1f, .25f, .75f);
                if (gameObject.GetComponentInChildren<ParticleSystem>() != null)
                {
                    gameObject.GetComponentInChildren<ParticleSystem>().Play();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponentInChildren<BonkableHead>().OnTrampoline = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            IsBouncing = false;
            if (collision.gameObject.activeInHierarchy)
            {
                collision.GetComponentInParent<PlayerMovement>().playerSpeed = collision.GetComponentInParent<PlayerMovement>().normalSpeed;
                //collision.GetComponentInParent<Animator>().SetBool("DoubleJumpActive", false);


            }
        }
        if (collision.tag == "Enemy")
        {
            collision.GetComponentInChildren<BonkableHead>().OnTrampoline = false;
        }
    }
}


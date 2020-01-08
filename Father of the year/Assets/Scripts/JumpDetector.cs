using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetector : MonoBehaviour
{
    public static bool OnGround;
    GameObject Player;

    public GameObject LandingEffect;
    public static GameObject LandingEffectClone;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet" && PlayerMovement.playerVelocity.y < -6)
        {
            if (Player.activeInHierarchy)
            {
                //collision.GetComponentInParent<PlayerMovement>().GetComponent<ParticleSystem>().Play();
                //collision.GetComponentInParent<PlayerMovement>().jumpAudioBox.playLandingSound();
                LandingEffectClone = Instantiate(LandingEffect);
                Destroy(LandingEffectClone, 1f);
                Boombox.SetVibrationIntensity(.1f, .2f, .2f); // vibrate a lil bit ;)

            }
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

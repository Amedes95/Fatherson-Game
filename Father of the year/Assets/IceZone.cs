using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceZone : MonoBehaviour
{
    public static bool OnIce;
    GameObject Player;
    public GameObject LandingEffect;
    public static GameObject LandingEffectClone;


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        OnIce = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet" && PlayerMovement.playerVelocity.y < -6)
        {
            if (Player.activeInHierarchy && FreezePlayer.Frozen == false)
            {
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
            if (!PlayerHealth.Dead && FreezePlayer.Frozen == false)
            {
                OnIce = true;
                PlayerMovement.jumpCount = 0;
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
                    OnIce = false;
                }

            }

        }
    }
}

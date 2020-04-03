using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public bool LavaSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (LavaSource && PlayerMovement.PlayerInvincible == false) // kills the player if lava source and not invincible
            {
                collision.GetComponent<PlayerHealth>().KillPlayer();
            }
            else if (!LavaSource) // kill player if not a lava source
            {
                collision.GetComponent<PlayerHealth>().KillPlayer();
            }
        }
        else if (collision.tag == "Feet" && PlayerMovement.PlayerInvincible == false)
        {
            if (collision.gameObject.activeInHierarchy)
            {
                if (LavaSource && PlayerMovement.PlayerInvincible == false) // kill player if your a lava source and player isn't invincible
                {
                    collision.GetComponentInParent<PlayerHealth>().KillPlayer();
                }
                else if (!LavaSource) // kill player if not lava source
                {
                    collision.GetComponentInParent<PlayerHealth>().KillPlayer();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (LavaSource && PlayerMovement.PlayerInvincible == false) // kills the player if lava source and not invincible
            {
                collision.GetComponent<PlayerHealth>().KillPlayer();
            }
            else if (!LavaSource) // kill player if not a lava source
            {
                collision.GetComponent<PlayerHealth>().KillPlayer();
            }
        }
        else if (collision.tag == "Feet" && PlayerMovement.PlayerInvincible == false)
        {
            if (collision.gameObject.activeInHierarchy)
            {
                if (LavaSource && PlayerMovement.PlayerInvincible == false) // kill player if your a lava source and player isn't invincible
                {
                    collision.GetComponentInParent<PlayerHealth>().KillPlayer();
                }
                else if (!LavaSource) // kill player if not lava source
                {
                    collision.GetComponentInParent<PlayerHealth>().KillPlayer();
                }
            }
        }
    }
}

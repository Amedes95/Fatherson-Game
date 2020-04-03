using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDetector : MonoBehaviour
{
    public bool KillEnemies;
    public bool LavaSource;

    // This function gets called when the player's collider touches the spike collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // make sure it's the player
        {
            if (LavaSource && PlayerMovement.PlayerInvincible == false)
            {
                collision.GetComponent<PlayerHealth>().KillPlayer(); // If lava source and not invincible, kill player
            }
            else if (!LavaSource)
            {
                collision.GetComponent<PlayerHealth>().KillPlayer(); // Kill player if not lava source regardless of buff
            }
        }
        if (collision.tag == "Enemy" && KillEnemies)
        {
            collision.GetComponentInChildren<BonkableHead>().SpawnDeathParticles();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") // make sure it's the player
        {
            if (LavaSource && PlayerMovement.PlayerInvincible == false)
            {
                collision.GetComponent<PlayerHealth>().KillPlayer(); // If lava source and not invincible, kill player
            }
            else if (!LavaSource)
            {
                collision.GetComponent<PlayerHealth>().KillPlayer(); // Kill player if not lava source regardless of buff
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDetector : MonoBehaviour
{

    // This function gets called when the player's collider touches the spike collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") // make sure it's the player
        {
            if (Goal.LevelComplete == false) // don't die if you've already won ;)
            {
                collision.GetComponent<PlayerHealth>().KillPlayer(); // I reference the HP component on the player and call one of its functions
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumble : MonoBehaviour
{
    public GameObject CrumbleChild;
    public bool Respawnable;
    public float SpawnDelay; // how long it is disabled for

    bool Disabled;
    float SpawnDelayReset;
    bool BlockingRespawn;

    private void Awake()
    {
        SpawnDelayReset = SpawnDelay;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet" || collision.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("Crumble");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Feet" || collision.tag == "Player")
        {
            BlockingRespawn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Feet" || collision.tag == "Player")
        {
            BlockingRespawn = false;
        }
    }


    public void DisableMe()
    {
        CrumbleChild.SetActive(false);
        Disabled = true;
    }

    private void FixedUpdate()
    {
        if (Respawnable) // only do this if I'm able to respawn
        {
            if (Disabled && !BlockingRespawn)
            {
                SpawnDelay -= Time.smoothDeltaTime;
                if (SpawnDelay <= 0)
                {
                    SpawnDelay = 0; // no negatives pls
                    Disabled = false;
                    SpawnDelay = SpawnDelayReset; // put it back time to the original start value
                    CrumbleChild.SetActive(true);
                    GetComponent<Animator>().SetTrigger("Respawn");
                }
            }
        }
    }
}

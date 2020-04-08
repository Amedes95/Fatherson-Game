using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int Value; // How nutritious is this fruit?
    public bool Respawnable;
    public float RespawnDelay;
    float RespawnDelayStart;
    bool Consumed;

    public bool NegatesLava;

    private void Awake()
    {
        RespawnDelayStart = RespawnDelay;
    }

    private void OnTriggerEnter2D(Collider2D collision) // Triggered when fruit is picked up by the player
    {
        if (collision.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("Collected"); // pop
            collision.GetComponent<Collector>().FruitFromLevel += Value; // The players current fruit plus the value of the fruit obtained
            if (!NegatesLava)
            {
                PlayerMovement.jumpCount = 1;
                collision.GetComponent<PlayerMovement>().SpawnDoubleJumpParticles();
            }
            else
            {
                PlayerMovement.InvincibilityTimer = 8f;
            }

            Consumed = true;
        }
    }

    private void FixedUpdate()
    {
        if (Consumed)
        {
            RespawnDelay -= Time.smoothDeltaTime;
            if (RespawnDelay <= 0)
            {
                GetComponent<Animator>().SetTrigger("Respawn");
                Consumed = false;
                RespawnDelay = RespawnDelayStart;
            }
        }
    }
}

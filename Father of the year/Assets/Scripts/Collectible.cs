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
    int ApplesEaten;
    int LollipopsEaten;

    public bool NegatesLava;

    private void Awake()
    {
        RespawnDelayStart = RespawnDelay;
        ApplesEaten = PlayerPrefs.GetInt("ApplesEaten");
        LollipopsEaten = PlayerPrefs.GetInt("LollipopsEaten");
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

                PlayerPrefs.SetInt("ApplesEaten", ApplesEaten += 1);

                // an apple a day achievement
                if (PlayerPrefs.GetInt("Doctor Repellent") == 0 && ApplesEaten >= 10)
                {
                    PlayerPrefs.SetInt("Doctor Repellent", 1);
                    Debug.Log("Doctor Repellent");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("Doctor Repellent");
                }
                // nutritious! achievement
                if (PlayerPrefs.GetInt("Nutritious!") == 0 && ApplesEaten >= 50)
                {
                    PlayerPrefs.SetInt("Nutritious!", 1);
                    Debug.Log("Nutritious! Unlocked");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("Nutritious!");
                }
            }
            else
            {
                PlayerMovement.InvincibilityTimer = 8f;
                PlayerPrefs.SetInt("LollipopsEaten", LollipopsEaten += 1);
                // Sugar Rush! achievement
                if (PlayerPrefs.GetInt("Sugar Rush!") == 0 && LollipopsEaten >= 15)
                {
                    PlayerPrefs.SetInt("Sugar Rush!", 1);
                    Debug.Log("Sugar Rush! Unlocked");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("Sugar Rush!");
                }

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

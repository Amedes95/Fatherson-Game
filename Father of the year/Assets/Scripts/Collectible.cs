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

    public bool Apple;
    public bool GoldCoin;

    public bool NegatesLava;

    private void Awake()
    {
        RespawnDelayStart = RespawnDelay;
        ApplesEaten = PlayerData.ApplesEaten;
        LollipopsEaten = PlayerData.LollipopsEaten;
    }

    private void OnTriggerEnter2D(Collider2D collision) // Triggered when fruit is picked up by the player
    {
        if (collision.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("Collected"); // pop
            collision.GetComponent<Collector>().FruitFromLevel += Value; // The players current fruit plus the value of the fruit obtained
            if (!NegatesLava)
            {
                if (Apple)
                {
                    PlayerMovement.jumpCount = 1;
                    collision.GetComponent<PlayerMovement>().SpawnDoubleJumpParticles();
                    PlayerData.ApplesEaten = ApplesEaten += 1;
                }
                else if (GoldCoin)
                {
                    GoldenDoor.CoinsToCollect -= 1;
                }


                // an apple a day achievement
                if (PlayerData.AchievementRecords.ContainsKey("Doctor Repellent") == false && ApplesEaten >= 10) // not already unlocked?
                {
                    PlayerData.AchievementRecords.Add("Doctor Repellent", 1); // add to unlock dictionary
                    Debug.Log("Doctor Repellent");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Doctor Repellent");
                }
                // nutritious! achievement
                if (PlayerData.AchievementRecords.ContainsKey("Nutritious!") == false && ApplesEaten >= 50) // not already unlocked?
                {
                    PlayerData.AchievementRecords.Add("Nutritious!", 1); // add to unlock dictionary
                    Debug.Log("Nutritious! Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Nutritious!");
                }
            }
            else
            {
                PlayerMovement.InvincibilityTimer = 8f;
                PlayerData.LollipopsEaten = LollipopsEaten += 1;
                // Sugar Rush! achievement
                if (PlayerData.AchievementRecords.ContainsKey("Sugar Rush!") == false && LollipopsEaten >= 15) // not already unlocked?
                {
                    PlayerData.AchievementRecords.Add("Sugar Rush!", 1); // add to unlock dictionary
                    Debug.Log("Sugar Rush! Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Sugar Rush!");
                }

            }


            Consumed = true;
        }
    }

    private void FixedUpdate()
    {
        if (Consumed)
        {
            if (Respawnable)
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    GameObject deathParticles;
    public static bool Dead; // it's static because I refence it directly from the playermovement && and death canvas script
    public static int DeathCount;
    int MalnourishedLives;

    void Awake() // we don't start dead when the scene loads now do we?
    {
        deathParticles = GameObject.FindGameObjectWithTag("DeathParticle");
        Dead = false;
        deathParticles.GetComponent<AudioSource>().playOnAwake = false;
        deathParticles.SetActive(false);
    }

    private void Update()
    {
        DeathCount = PlayerData.PD.LifetimeDeaths;
    }

    public void KillPlayer() // Kills player
    {
        Boombox.SetVibrationIntensity(.1f, .5f, .5f);
        deathParticles.GetComponent<AudioSource>().playOnAwake = true;
        Dead = true; // oof
        deathParticles.transform.position = gameObject.transform.position;
        deathParticles.SetActive(true);
        gameObject.SetActive(false);
        PlayerData.PD.LifetimeDeaths = PlayerData.PD.LifetimeDeaths += 1; // update lifetime deaths
        PlayerPrefs.SetInt("Flawless Run", 1); // voids achievement if dead.  Only resets on start of level 1

        if (PlayerPrefs.GetInt("MalnourishedMode") == 1)
        {
            MalnourishedLives = PlayerPrefs.GetInt("MalnourishedLives");
            MalnourishedLives -= 1;
            PlayerPrefs.SetInt("MalnourishedLives", MalnourishedLives);
            Debug.Log("Malnourished lives" + PlayerPrefs.GetInt("MalnourishedLives"));
        }


        /// player dies for the first time achievement
        if (PlayerData.PD.AchievementRecords.ContainsKey("Let's try that again") == false && DeathCount >= 0) // not unlocked already?
        {
            PlayerData.PD.AchievementRecords.Add("Let's try that again", 1); // add to unlock dictionary
            Debug.Log("Let's try that again");
            BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
            BGMusic.UnlockCheevo("Let's try that again");
        }

        /// die 20 times achievement
        if (PlayerData.PD.AchievementRecords.ContainsKey("20th time's the charm") == false && DeathCount >= 19) // not unlocked already?
        {
            PlayerData.PD.AchievementRecords.Add("20th time's the charm", 1); // add to unlock dictionary
            Debug.Log("20th time's the charm");
            BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
            BGMusic.UnlockCheevo("20th time's the charm");
        }
        /// die 200 times achievement
        if (PlayerData.PD.AchievementRecords.ContainsKey("Lucky 200") == false && DeathCount >= 199) // not unlocked already?
        {
            PlayerData.PD.AchievementRecords.Add("Lucky 200", 1);
            Debug.Log("Lucky 200");
            BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
            BGMusic.UnlockCheevo("Lucky 200");
        }
        PlayerData.PD.SavePlayer();
        //Debug.Log(DeathCount);


    }
}

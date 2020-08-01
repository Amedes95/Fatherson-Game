﻿using System.Collections;
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
        DeathCount = PlayerPrefs.GetInt("DeathCount");
    }

    public void KillPlayer() // Kills player
    {
        Boombox.SetVibrationIntensity(.1f, .5f, .5f);
        deathParticles.GetComponent<AudioSource>().playOnAwake = true;
        Dead = true; // oof
        deathParticles.transform.position = gameObject.transform.position;
        deathParticles.SetActive(true);
        gameObject.SetActive(false);
        PlayerPrefs.SetInt("DeathCount", DeathCount += 1);
        PlayerPrefs.SetInt("Flawless Run", 1); // voids achievement if dead.  Only resets on start of level 1

        if (PlayerPrefs.GetInt("MalnourishedMode") == 1)
        {
            MalnourishedLives = PlayerPrefs.GetInt("MalnourishedLives");
            MalnourishedLives -= 1;
            PlayerPrefs.SetInt("MalnourishedLives", MalnourishedLives);
            Debug.Log("Malnourished lives" + PlayerPrefs.GetInt("MalnourishedLives"));
        }


        /// player dies for the first time achievement
        if (PlayerPrefs.GetInt("Let's try that again") == 0 && DeathCount == 1)
        {
            PlayerPrefs.SetInt("Let's try that again", 1);
            Debug.Log("Let's try that again");
            BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
            BGMusic.UnlockCheevo("Let's try that again");
        }

        /// die 20 times achievement
        if (PlayerPrefs.GetInt("20th time's the charm") == 0 && DeathCount == 20)
        {
            PlayerPrefs.SetInt("20th time's the charm", 1);
            Debug.Log("20th time's the charm");
            BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
            BGMusic.UnlockCheevo("20th time's the charm");
        }
        /// die 200 times achievement
        if (PlayerPrefs.GetInt("Lucky 200") == 0 && DeathCount == 200)
        {
            PlayerPrefs.SetInt("Lucky 200", 1);
            Debug.Log("Lucky 200");
            BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
            BGMusic.UnlockCheevo("Lucky 200");
        }

        Debug.Log(DeathCount);
        

    }
}

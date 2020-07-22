﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public GameObject VeganConfirmation;
    public GameObject MalnourishedConfirmation;
    public GameObject BossRushConfirmation;
    // add a best time for vegan mode?
    public TextMeshProUGUI DeathsText;
    public TextMeshProUGUI TrophiesText;
    public TextMeshProUGUI ApplesText;
    public TextMeshProUGUI EnemiesText;


    public void AskToConfirmVeganMode()
    {
        VeganConfirmation.SetActive(true);

    }
    public void AskToConfirmMalnourishedMode()
    {
        MalnourishedConfirmation.SetActive(true);

    }
    public void ASkToConfirmBossRush()
    {
        BossRushConfirmation.SetActive(true);
    }

    public void BeginVeganMode()
    {
        PlayerPrefs.SetInt("VeganMode", 1); // toggle bool
        PlayerPrefs.SetFloat("VeganTimer", 0); // reset timer

        SceneManager.LoadScene("Tutorial_01");
    }
    public void BeginMalnourishedMode()
    {
        PlayerPrefs.SetInt("MalnourishedMode", 1); // toggle bool
        PlayerPrefs.SetInt("MalnourishedLives", 3); // give player 3 lives
        SceneManager.LoadScene("Tutorial_01");
    }
    public void BeginBossRush()
    {
        PlayerPrefs.SetInt("BossRush", 1);
        SceneManager.LoadScene("W1BOSS");      
    }

    public void DenyVeganConfirmation()
    {
        VeganConfirmation.SetActive(false);
    }
    public void DenyMalnourishedConfirmation()
    {
        MalnourishedConfirmation.SetActive(false);
    }
    public void DenyBossRush()
    {
        BossRushConfirmation.SetActive(false);
    }

    private void Update()
    {
        DeathsText.text = PlayerPrefs.GetInt("DeathCount").ToString();
        TrophiesText.text = PlayerPrefs.GetInt("GoldMedalsEarned").ToString();
        ApplesText.text = PlayerPrefs.GetInt("ApplesEaten").ToString();
        EnemiesText.text = PlayerPrefs.GetInt("EnemiesKilled").ToString();

    }
}

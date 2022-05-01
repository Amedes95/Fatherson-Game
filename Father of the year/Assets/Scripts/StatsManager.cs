using System.Collections;
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
    public UIControllerSupport BackButton;


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
        BackButton.FindFocus();
    }
    public void DenyMalnourishedConfirmation()
    {
        MalnourishedConfirmation.SetActive(false);
        BackButton.FindFocus();
    }
    public void DenyBossRush()
    {
        BossRushConfirmation.SetActive(false);
        BackButton.FindFocus();
    }

    private void Update()
    {
        DeathsText.text = PlayerData.LifetimeDeaths.ToString();
        TrophiesText.text = PlayerData.TotalGoldMedals.ToString();
        ApplesText.text = PlayerData.ApplesEaten.ToString();
        EnemiesText.text = PlayerData.EnemiesKilled.ToString();

    }
}

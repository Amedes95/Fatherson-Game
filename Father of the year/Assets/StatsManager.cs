using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public GameObject VeganConfirmation;
    public TextMeshProUGUI DeathsText;
    public TextMeshProUGUI TrophiesText;
    public TextMeshProUGUI ApplesText;
    public TextMeshProUGUI EnemiesText;



    public void AskToConfirmVeganMode()
    {
        VeganConfirmation.SetActive(true);
    }
    public void BeginVeganMode()
    {
        PlayerPrefs.SetInt("VeganMode", 1); // toggle bool
        PlayerPrefs.SetFloat("VeganTimer", 0); // reset timer

        SceneManager.LoadScene("Tutorial_01");
    }

    public void DenyVeganConfirmation()
    {
        VeganConfirmation.SetActive(false);
    }

    private void Update()
    {
        DeathsText.text = PlayerPrefs.GetInt("DeathCount").ToString();
        TrophiesText.text = PlayerPrefs.GetInt("GoldMedalsEarned").ToString();
        ApplesText.text = PlayerPrefs.GetInt("ApplesEaten").ToString();
        EnemiesText.text = PlayerPrefs.GetInt("EnemiesKilled").ToString();

    }
}

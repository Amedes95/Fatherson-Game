using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
    public string LevelDisplayName;
    public string WorldDisplayName;
    public string SceneToLoad;
    public GameObject CompletedTrophy;
    public bool Unlocked;
    public GameObject LockedSymbol;

    public TextMeshPro BestTime;
    float BestTIme;


    public float GoldStandard;
    //public float SilverStandard;
    public float BronzeStandard;

    public bool GoldTierAchieved;
    bool SilverTierAchieved;
    bool BronzeTierAchieved;
    public static int GoldMedalsEarned;


    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Active");
        if (Unlocked)
        {
            gameObject.GetComponent<Animator>().SetBool("Active", true);
        }


        //// for completion
        if (PlayerData.PD.PlayerTimeRecords.ContainsKey(SceneToLoad)) // has this level been completed before?
        {
            BestTIme = PlayerData.PD.GetLevelBestTime(SceneToLoad); // update best time
            string hours = Mathf.Floor(BestTIme / 60 / 60).ToString("00");
            string minutes = Mathf.Floor((BestTIme / 60) % 60).ToString("00");
            string seconds = (BestTIme % 60).ToString("00");

            BestTime.text = hours + ":" + minutes + ":" + seconds;

            CompletedTrophy.SetActive(true); // Display Trophy if beaten!

            // How do you stack up?

            //// GOLD?
            if (PlayerData.PD.GetLevelBestTime(SceneToLoad) <= GoldStandard) // if your time is faster than the set gold standard
            {
                //Debug.Log("GOLD TIER" + PlayerPrefs.GetFloat(SceneToLoad).ToString("F2"));
                CompletedTrophy.GetComponent<Animator>().SetTrigger("Gold");
            }
            //// SILVER?
            else if (PlayerData.PD.GetLevelBestTime(SceneToLoad) > GoldStandard && PlayerData.PD.GetLevelBestTime(SceneToLoad) <= BronzeStandard)
            {
                //Debug.Log("SILVER TIER" + PlayerPrefs.GetFloat(SceneToLoad).ToString("F2"));
                CompletedTrophy.GetComponent<Animator>().SetTrigger("Silver");
            }
            //// BRONZE?
            else if (PlayerData.PD.GetLevelBestTime(SceneToLoad) > BronzeStandard)
            {
                //Debug.Log("BRONZE TIER" + PlayerPrefs.GetFloat(SceneToLoad).ToString("F2"));
                CompletedTrophy.GetComponent<Animator>().SetTrigger("Bronze");
            }


        }
        else
        {
            CompletedTrophy.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<Animator>().SetBool("Active", false);
    }

    private void FixedUpdate()
    {

        // locked symbol
        if (Unlocked)
        {
            LockedSymbol.SetActive(false);
            //// GOLD?
            if (PlayerData.PD.GetLevelBestTime(SceneToLoad) <= GoldStandard && PlayerData.PD.PlayerTimeRecords.ContainsKey(SceneToLoad)) // if your time is faster than the set gold standard
            {
                GoldTierAchieved = true;
                //PlayerPrefs.SetInt("GoldMedalsEarned", GoldMedalsEarned += 1);
                //Debug.Log(GoldMedalsEarned);
            }
        }
        else
        {
            LockedSymbol.SetActive(true);
        }
    }

    private void Start()
    {
        //// GOLD?
        if (PlayerData.PD.GetLevelBestTime(SceneToLoad) <= GoldStandard && PlayerData.PD.PlayerTimeRecords.ContainsKey(SceneToLoad)) // if your time is faster than the set gold standard
        {
            GoldTierAchieved = true;
        }
    }

}

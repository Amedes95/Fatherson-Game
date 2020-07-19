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

    public float GoldStandard;
    //public float SilverStandard;
    public float BronzeStandard;

    bool GoldTierAchieved;
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
        if (PlayerPrefs.GetFloat(SceneToLoad) != 0)
        {
            BestTime.text = PlayerPrefs.GetFloat(SceneToLoad).ToString("F2"); // update best time text (F2 rounds the string to 2 decimals)
            CompletedTrophy.SetActive(true); // Display Trophy if beaten!

            // How do you stack up?

            //// GOLD?
            if (PlayerPrefs.GetFloat(SceneToLoad) <= GoldStandard) // if your time is faster than the set gold standard
            {
                //Debug.Log("GOLD TIER" + PlayerPrefs.GetFloat(SceneToLoad).ToString("F2"));
                CompletedTrophy.GetComponent<Animator>().SetTrigger("Gold");
            }
            //// SILVER?
            else if (PlayerPrefs.GetFloat(SceneToLoad) > GoldStandard && PlayerPrefs.GetFloat(SceneToLoad) <= BronzeStandard)
            {
                //Debug.Log("SILVER TIER" + PlayerPrefs.GetFloat(SceneToLoad).ToString("F2"));
                CompletedTrophy.GetComponent<Animator>().SetTrigger("Silver");
            }
            //// BRONZE?
            else if (PlayerPrefs.GetFloat(SceneToLoad) > BronzeStandard)
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
        }
        else
        {
            LockedSymbol.SetActive(true);
        }
    }

    private void Awake()
    {

        //// GOLD?
        if (PlayerPrefs.GetFloat(SceneToLoad) <= GoldStandard && PlayerPrefs.GetFloat(SceneToLoad) != 0) // if your time is faster than the set gold standard
        {
            PlayerPrefs.SetInt("GoldMedalsEarned", GoldMedalsEarned += 1);
            //Debug.Log("gold trophy");
        }

        /// Unlocks Oooh Shiny! Achievement
        if (PlayerPrefs.GetInt("Oooh Shiny!") == 0 && GoldMedalsEarned >= 1)
        {
            PlayerPrefs.SetInt("Oooh Shiny!", 1);
            Debug.Log("Oooh Shiny! Unlocked");
            Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
            Boombox.UnlockCheevo("Oooh Shiny!");
        }
        /// Unlocks Gold Medalist Achievement
        if (PlayerPrefs.GetInt("Gold Medalist") == 0 && GoldMedalsEarned >= 140)
        {
            PlayerPrefs.SetInt("Gold Medalist", 1);
            Debug.Log("Gold Medalist Unlocked");
            Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
            Boombox.UnlockCheevo("Gold Medalist");
        }
    }

}

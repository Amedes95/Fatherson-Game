using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class SecretGoal : MonoBehaviour
{

    VictoryMenu VictoryScreen;
    //public string SecretLevelName;

    public bool PartyPortal;
    public bool OldTimeyPortal;
    public bool GoldPortal;

    public bool Partying;
    public bool BeingOld;
    public PostProcessingProfile Transition1;




    // Start is called before the first frame update
    void Awake()
    {
        VictoryScreen = GameObject.FindGameObjectWithTag("VictoryMenu").GetComponent<VictoryMenu>();
        if (Partying)
        {
            VisualsScreen.Partying = true;
            PlayerPrefs.SetString("ExitedLevel", "W2L08");
        }
        else if (BeingOld)
        {
            VisualsScreen.BeingOld = true;
            PlayerPrefs.SetString("ExitedLevel", "W4L09");

        }
        if (PlayerPrefs.GetInt("PartyModeON") == 0 && Partying)
        {
            TogglePartyMode();
        }
        if (PlayerPrefs.GetInt("OldTimeyON") == 0 && BeingOld)
        {
            ToggleOldTimerMode();
        }

        if (PartyPortal)
        {
            if (VisualsScreen.Partying == true)
            {
                TogglePartyMode();
            }
            VisualsScreen.Partying = false;

        }
        else if (OldTimeyPortal)
        {
            if (VisualsScreen.BeingOld == true)
            {
                ToggleOldTimerMode();
            }
            VisualsScreen.BeingOld = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PartyPortal) // party portal, yeah baby
            {
                VictoryScreen.NextLevel = "Party01";
                /// Unlocks Party Animal Achievement
                if (PlayerPrefs.GetInt("Party Animal") == 0)
                {
                    PlayerPrefs.SetInt("Party Animal", 1);
                    Debug.Log("Party Animal Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Party Animal");
                }
            }
            else if (OldTimeyPortal) // huh, what that sonny?
            {
                VictoryScreen.NextLevel = "Retro01";
                /// Unlocks Fossilized Achievement
                if (PlayerPrefs.GetInt("Pyroclastic") == 0)
                {
                    PlayerPrefs.SetInt("Pyroclastic", 1);
                    Debug.Log("Pyroclastic");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Pyroclastic");
                }
            }
            else if (GoldPortal) // SHINY!
            {
                VictoryScreen.NextLevel = "Gold01";
                // unlocks Jackpot! achievement
                if (PlayerPrefs.GetInt("Jackpot!") == 0)
                {
                    PlayerPrefs.SetInt("Jackpot!", 1);
                    Debug.Log("Jackpot!");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Jackpot!");
                }
            }
            gameObject.GetComponent<Animator>().SetTrigger("Complete");
            Boombox.SetVibrationIntensity(.5f, .25f, .25f);
            collision.gameObject.SetActive(false);
            VictoryScreen.LoadNextLevel();

        }

    }

    public void TogglePartyMode()
    {
        if (PlayerPrefs.GetInt("OldTimeyON") == 1)
        {
            ToggleOldTimerMode();
        }
        PlayerPrefs.SetInt("OldTimeyON", 0); // toggle off old timey mode
        if (PlayerPrefs.GetInt("PartyModeON") == 1) // turn party off... :(
        {
            PlayerPrefs.SetInt("PartyModeON", 0);
            PlayerPrefs.SetInt("Party Run", 0); // cancel party run, no cheating
        }
        else
        {
            PlayerPrefs.SetInt("PartyModeON", 1); // Part on baby
            var Hue = Transition1.colorGrading.settings;
            Hue.basic.hueShift = 180;
        }
        // helps change the music
        Boombox CurrentBoombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
        CurrentBoombox.UpdateSound();
    }
    public void ToggleOldTimerMode()
    {
        if (PlayerPrefs.GetInt("PartyModeON") == 1) // toggle off party mode
        {
            TogglePartyMode();
        }

        if (PlayerPrefs.GetInt("OldTimeyON") == 1)
        {
            PlayerPrefs.SetInt("OldTimeyON", 0);
            PlayerPrefs.SetInt("Party Run", 0); // cancel party run
            var Grainy = Transition1.grain.settings;
            Grainy.intensity = 0;
            Grainy.size = 3;
            Transition1.grain.settings = Grainy;
        }
        else
        {
            PlayerPrefs.SetInt("OldTimeyON", 1);
            var Hue = Transition1.colorGrading.settings;
            Hue.basic.hueShift = 7;
        }
        // helps change the music
        Boombox CurrentBoombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
        CurrentBoombox.UpdateSound();
    }
}

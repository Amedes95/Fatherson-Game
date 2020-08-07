using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;
using TMPro;


public class VisualsScreen : MonoBehaviour
{
    public GameObject ToggleChromaButton;
    public GameObject OffSymbol;
    public GameObject OnSymbol;
    public GameObject OffSymbolParty;
    public GameObject OnSymbolParty;
    public GameObject OffOldTime;
    public GameObject OnOldTime;

    public PostProcessingProfile Transition1;


    // party / old timey worlds
    public static bool Partying;
    public static bool BeingOld;

    public Button PartyModeButton;
    public GameObject LockSymbolParty;

    public Button OldTimeyModeButton;
    public GameObject LockSymbolOld;

    public TextMeshProUGUI PartyText;
    public TextMeshProUGUI OldText;


    private void Awake()
    {
        var Hue = Transition1.colorGrading.settings;

        if (Partying || BeingOld)
        {
            PartyModeButton.interactable = false;
            OldTimeyModeButton.interactable = false;
            LockSymbolOld.SetActive(true);
            LockSymbolParty.SetActive(true);
        }
        else
        {
            PartyModeButton.interactable = true;
            OldTimeyModeButton.interactable = true;
            LockSymbolOld.SetActive(false);
            LockSymbolParty.SetActive(false);
        }

        // unlock party mode in visual settings
        if (PlayerPrefs.GetInt("PartyUnlocked") == 0)
        {
            PartyModeButton.interactable = false;
            LockSymbolParty.SetActive(true);
            PartyText.text = "?????";
        }
        else
        {
            PartyText.text = "Party Mode";
        }
        if (PlayerPrefs.GetInt("PartyUnlocked") == 1 && !Partying && !BeingOld)
        {
            PartyModeButton.interactable = true;
            LockSymbolParty.SetActive(false);
        }

        // unlocks old timer mode when achievement is unlocked
        if (PlayerPrefs.GetInt("OldTimeyUnlocked") == 0)
        {
            OldTimeyModeButton.interactable = false;
            LockSymbolOld.SetActive(true);
            OldText.text = "?????";
        }
        else
        {
            OldText.text = "Retro";
        }
        if (PlayerPrefs.GetInt("OldTimeyUnlocked") == 1 && !BeingOld && !Partying)
        {
            OldTimeyModeButton.interactable = true;
            LockSymbolOld.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /// settings for chromatic abberation
        if (PlayerPrefs.GetInt("ChromaON") == 0) // chroma enabled
        {
            OnSymbol.SetActive(true);
            OffSymbol.SetActive(false);
        }
        else // disabled
        {
            OffSymbol.SetActive(true);
            OnSymbol.SetActive(false);
        }

        //settings for Party Mode
        if (PlayerPrefs.GetInt("PartyModeON") == 1) // party time
        {
            OnSymbolParty.SetActive(true);
            OffSymbolParty.SetActive(false);
        }
        else // disabled
        {
            OffSymbolParty.SetActive(true);
            OnSymbolParty.SetActive(false);
        }
        //settings for old Mode
        if (PlayerPrefs.GetInt("OldTimeyON") == 1) // old mode time
        {
            OnOldTime.SetActive(true);
            OffOldTime.SetActive(false);
        }
        else // disabled
        {
            OffOldTime.SetActive(true);
            OnOldTime.SetActive(false);
        }
    }

    public void ToggleChroma()
    {
        if (PlayerPrefs.GetInt("ChromaON") == 1) // if on, turn off
        {
            PlayerPrefs.SetInt("ChromaON", 0);
        }
        else
        {
            PlayerPrefs.SetInt("ChromaON", 1);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;


public class VisualsScreen : MonoBehaviour
{
    public GameObject ToggleChromaButton;
    public GameObject OffSymbol;
    public GameObject OnSymbol;
    public GameObject OffSymbolParty;
    public GameObject OnSymbolParty;

    public PostProcessingProfile Transition1;


    private void Awake()
    {
        var Hue = Transition1.colorGrading.settings;
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

    }
}

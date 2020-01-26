using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;


public class PartyMode : MonoBehaviour
{
    public float PartySpeed;

    public PostProcessingProfile Transition1;
    bool Rising;


    private void Awake()
    {
        var Hue = Transition1.colorGrading.settings;
        var Blurry = Transition1.depthOfField.settings;
        Blurry.focalLength = 0f;
        Transition1.depthOfField.settings = Blurry;
    }

    // Update is called once per frame
    void Update()
    {
        // Party Mode
        var Hue = Transition1.colorGrading.settings;
        if (PlayerPrefs.GetInt("PartyModeON") == 1)
        {
            Transition1.colorGrading.enabled = true;
            if (Hue.basic.hueShift == 180)
            {
                Rising = false;
            }
            else if (Hue.basic.hueShift == -180)
            {
                Rising = true;
            }
            // rises and lowers hue
            if (Rising)
            {
                Hue.basic.hueShift += PartySpeed;
                Transition1.colorGrading.settings = Hue;
            }
            else
            {
                Hue.basic.hueShift -= PartySpeed;
                Transition1.colorGrading.settings = Hue;
            }
        }
        else
        {
            Transition1.colorGrading.enabled = false;
        }
    }
}

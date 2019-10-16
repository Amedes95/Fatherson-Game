using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraEffects : MonoBehaviour
{
    public PostProcessingProfile Transition1;
    bool Complete;
    public float ShadowValueUp;

    private void Awake()
    {
        Complete = false;
        //var Vinny = Transition1.vignette.settings;
        //Vinny.intensity = 0f;
        //Transition1.vignette.settings = Vinny;
    }

    // Update is called once per frame
    void Update()
    {
        var Vinny = Transition1.vignette.settings;
        if (Input.GetKeyDown(KeyCode.O))
        {
            Complete = true;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Complete = false;
        }

        if (Complete)
        {
            Vinny.intensity += ShadowValueUp;
            if (Vinny.intensity >= 1)
            {
                Vinny.intensity = 1;
            }
        }
        else
        {
            Vinny.intensity -= (ShadowValueUp + .02f);
            if (Vinny.intensity <= 0)
            {
                Vinny.intensity = 0;
            }
        }
        Transition1.vignette.settings = Vinny;
    }
}

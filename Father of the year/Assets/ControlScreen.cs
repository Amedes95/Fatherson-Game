using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScreen : MonoBehaviour
{

    public GameObject XboxButtons;
    public GameObject MouseButtons;



    private void Update()
    {
        if (Boombox.ControllerModeEnabled)
        {
            XboxButtons.SetActive(true);
            MouseButtons.SetActive(false);
        }
        else
        {
            MouseButtons.SetActive(true);
            XboxButtons.SetActive(false);

        }
    }
}


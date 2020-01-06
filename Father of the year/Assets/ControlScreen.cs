using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class ControlScreen : MonoBehaviour
{

    public GameObject XboxButtons;
    public GameObject MouseButtons;

    public GameObject RumbleOn;
    public GameObject RumbleOff;

    public Button ToggleButton;


    private void Update()
    {
        if (Boombox.ControllerModeEnabled)
        {
            ToggleButton.enabled = true;
            XboxButtons.SetActive(true);
            MouseButtons.SetActive(false);
            if (PlayerPrefs.GetFloat("RumbleToggled") == 1) // disconnected but still on
            {
                RumbleOn.SetActive(true);
                RumbleOff.SetActive(false);
            }
        }
        else
        {
            ToggleButton.enabled = false;
            MouseButtons.SetActive(true);
            XboxButtons.SetActive(false);

            RumbleOn.SetActive(false);
            RumbleOff.SetActive(true);

        }
    }

    private void OnEnable()
    {
        if (PlayerPrefs.GetFloat("RumbleToggled") == 1 && Boombox.ControllerModeEnabled)
        {
            RumbleOn.SetActive(true);
            RumbleOff.SetActive(false);
        }
        else
        {
            RumbleOn.SetActive(false);
            RumbleOff.SetActive(true);
        }
    }



    public void ToggleRumble()
    {
        if (PlayerPrefs.GetFloat("RumbleToggled") == 1 && Gamepad.current != null)
        {
            Boombox.SetVibrationIntensity(0f, .25f, .25f);
            PlayerPrefs.SetFloat("RumbleToggled", 0);

            RumbleOn.SetActive(false);
            RumbleOff.SetActive(true);

        }
        else if (PlayerPrefs.GetFloat("RumbleToggled") == 0 && Gamepad.current != null)
        {
            RumbleOn.SetActive(true);
            RumbleOff.SetActive(false);

            PlayerPrefs.SetFloat("RumbleToggled", 1);
            Boombox.SetVibrationIntensity(.1f, .25f, .25f);
        }
    }

}


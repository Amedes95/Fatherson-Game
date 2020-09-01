using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class ControlScreen : MonoBehaviour
{

    public GameObject XboxButtons;
    public GameObject PS4Buttons;
    public GameObject MouseButtons;

    public GameObject RumbleOn;
    public GameObject RumbleOff;

    public Button ToggleButton;
    public GameObject ControllerConnected;
    public GameObject ControllerDisconnected;


    private void Update()
    {
        if (Boombox.ControllerModeEnabled)
        {
            ControllerConnected.SetActive(true);
            ControllerDisconnected.SetActive(false);
            ToggleButton.enabled = true;
            if (Boombox.PS4Enabled)
            {
                PS4Buttons.SetActive(true);
            }
            else
            {
                XboxButtons.SetActive(true);
            }
            MouseButtons.SetActive(false);
            if (PlayerPrefs.GetFloat("RumbleToggled") == 1) // disconnected but still on
            {
                RumbleOn.SetActive(true);
                RumbleOff.SetActive(false);
            }
        }
        else
        {
            ControllerConnected.SetActive(false);
            ControllerDisconnected.SetActive(true);
            ToggleButton.enabled = false;
            MouseButtons.SetActive(true);
            XboxButtons.SetActive(false);
            PS4Buttons.SetActive(false);

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


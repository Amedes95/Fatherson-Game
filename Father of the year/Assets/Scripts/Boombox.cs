﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;


public class Boombox : MonoBehaviour
{
    public AudioClip LevelMusic;
    BackgroundMusic BGMusic;
    public GameObject BGPrefab;
    public static bool EditorMode;

    public static bool ControllerModeEnabled;

    public static float vibrateDuration = 0f;
    public static float LowSpeed;
    public static float HighSpeed;

    bool RumbleEnabled;

    public TextMeshProUGUI CheevoText;



    // Start is called before the first frame update
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        if (GameObject.FindGameObjectWithTag("BGMusic") == null)
        {
            EditorMode = true;
            Instantiate(BGPrefab);
            BGPrefab.GetComponent<BackgroundMusic>().LevelMusic = LevelMusic;
            BGPrefab.GetComponent<BackgroundMusic>().CompareSongs();
        }
        else
        {
            BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
            BGMusic.CompareSongs();
        }
    }

    private void Update()
    {  
        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        //Check whether array contains anything
        if (temp.Length > 0)
        {
            //Iterate over every element
            for (int i = 0; i < temp.Length; ++i)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    //Not empty, controller temp[i] is connected
                    ControllerModeEnabled = true;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    //Debug.Log("Controller Used:" + temp[i].ToString());

                    // This is a shitty way of identifying what controller you have, but it works
                    if (temp[i].ToString() == "Controller (Xbox One For Windows)")
                    {
                        //Debug.Log("That's an xbox controller plugged in");
                    }

                }
                else
                {
                    ControllerModeEnabled = false;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number
                }
            }
        }

        if (ControllerModeEnabled) // controller is physically plugged in
        {
            RumbleEnabled = true; // always enable if controller is physically plugged in

            if (RumbleEnabled && PlayerPrefs.GetFloat("RumbleToggled") == 1 && PauseMenu.GameIsPaused == false) // but we don't enable rumnble unless it is toggled on
            {
                if (vibrateDuration > 0 && RumbleEnabled) // something was called to vibrate
                {
                    Gamepad.current.ResumeHaptics();
                    vibrateDuration -= Time.smoothDeltaTime; // count down duration
                    VibrateController(); // vibrate while counting down
                }
                else if (vibrateDuration <= 0)
                {
                    vibrateDuration = 0;
                    if (Gamepad.current != null)
                    {
                        Gamepad.current.PauseHaptics();
                    }
                }
            }


        }
        else
        {
            RumbleEnabled = false;
        }
    }

    public void VibrateController() // causes vibration of controller
    {
        Gamepad.current.SetMotorSpeeds(LowSpeed, HighSpeed);
    }

    public static void SetVibrationIntensity(float duration, float lowerSpeed, float higherspeed) // sets the variables accordingly
    {
        vibrateDuration = duration;
        LowSpeed = lowerSpeed;
        HighSpeed = higherspeed;
    }


    public void UnlockCheevo(string CheevoName)
    {
        //PlayerPrefs.SetInt(CheevoName, 1); // updates player prefs with unlock
        CheevoText.text = "Achievement Unlocked: " + CheevoName;
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Unlock");
    }

}

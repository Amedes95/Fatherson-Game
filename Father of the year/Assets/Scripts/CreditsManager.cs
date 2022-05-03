﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class CreditsManager : MonoBehaviour
{
    public GameObject CreditsScreen;
    public GameObject SkipCanvas;

    bool AtCreditsArea;
    bool WatchingOutro;

    GameObject Camera;
    Transform CurrentDestination; // this one gets set by the others

    public Transform CreditsDestination;
    public Transform IntroDestination;

    public float CameraSpeed;

    public GameObject Credits;
    bool Skipping;
    public GameObject GoalParent;
    StandaloneInputModule Inputs;


    private void Update()
    {
        if (Boombox.ControllerModeEnabled)
        {
            if (Boombox.PS4Enabled)
            {
                if (Application.platform != (RuntimePlatform.LinuxPlayer) && Application.platform != (RuntimePlatform.LinuxEditor)) // if not Linux, change controls
                {
                    Inputs = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<StandaloneInputModule>();
                    Inputs.submitButton = "PS4Submit";
                    Inputs.cancelButton = "PS4Cancel";
                }
                else // Linux
                {
                    Inputs = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<StandaloneInputModule>();
                    Inputs.submitButton = "Submit";
                    Inputs.cancelButton = "Cancel";
                }

            }
            else
            {
                Inputs = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<StandaloneInputModule>();
                Inputs.cancelButton = "Cancel";
                Inputs.submitButton = "Submit";
            }
        }
        else
        {
            Inputs = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<StandaloneInputModule>();
            Inputs.cancelButton = "Cancel";
            Inputs.submitButton = "Submit";
        }


        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, CurrentDestination.position, CameraSpeed); // constantly move the camera to the "Current Destination"
        if (( Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause") || Input.GetButtonDown("PS4Pause")) && GoalParent.transform.localPosition.y != 9.5 && Skipping == false) // skips intro
        {
            SkipCanvas.SetActive(true);
        }
        if (Camera.transform.position == CurrentDestination.position) // we made it, decide from here
        {
            if (AtCreditsArea) // "Credits Menu"
            {
                CreditsScreen.SetActive(true);
                //SkipCanvas.SetActive(false);
                Credits.SetActive(true);
            }
        }
        if (Skipping)
        {
            Credits.GetComponent<Animator>().SetBool("Skip", true);
        }
        if (GoalParent.transform.localPosition.y == 9.5)
        {
            SkipCanvas.SetActive(false);
        }
    }

    public void LoadWorldHub() // Loads world hub scene
    {
        SceneManager.LoadScene("WorldHub");
    }

    private void Awake()
    {

        PlayerData.PD.GameCompleted = 1;
        Time.timeScale = 1f;
        /// Unlocks Carnist Achievement
        if (PlayerData.PD.AchievementRecords.ContainsKey("Carnist") == false && PlayerPrefs.GetInt("VeganMode") == 1) // not unlocked already?
        {
            PlayerData.PD.AchievementRecords.Add("Carnist", 1); // add to unlock dictionary
            Debug.Log("Carnist Unlocked");
            BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
            BGMusic.UnlockCheevo("Carnist");
        }

        if (PlayerPrefs.GetFloat("VeganTimer") <= 7200 && PlayerPrefs.GetInt("VeganMode") == 1) // if credits are reached under 2 hours
        {
            /// Unlocks Fast Food Achievement
            if (PlayerData.PD.AchievementRecords.ContainsKey("Fast Food") == false) // not unlocked already?
            {
                PlayerData.PD.AchievementRecords.Add("Fast Food", 1); // add to unlock dictionary
                Debug.Log("Fast Food Unlocked");
                BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                BGMusic.UnlockCheevo("Fast Food");
            }
        }
        if (PlayerPrefs.GetFloat("Insatiable Appetite") <= 7200 && PlayerPrefs.GetInt("MalnourishedMode") == 1) // if credits are reached during malnourished mode
        {
            /// Unlocks Insatiable Appetite Achievement
            if (PlayerData.PD.AchievementRecords.ContainsKey("Insatiable Appetite") == false) // not unlocked already?
            {
                PlayerData.PD.AchievementRecords.Add("Insatiable Appetite", 1); // add to unlock dictionary
                Debug.Log("Insatiable Appetite");
                BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                BGMusic.UnlockCheevo("Insatiable Appetite");
            }
        }
        if (PlayerPrefs.GetInt("BossRush") == 1)
        {
            /// Unlocks Indigestible Achievement
            if (PlayerData.PD.AchievementRecords.ContainsKey("Indigestible") == false) // not unlocked already?
            {
                PlayerData.PD.AchievementRecords.Add("Indigestible", 1); // add to unlock dictionary
                Debug.Log("Indigestible");
                BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                BGMusic.UnlockCheevo("Indigestible");
            }
        }

        PlayerPrefs.SetInt("VeganMode", 0); // stops vegan mode
        PlayerPrefs.SetInt("MalnourishedMode", 0); // stop malnourished mode
        PlayerPrefs.SetInt("BosRush", 0); // stop bosh rush mode

    }

    public void Start()
    {
        CreditsScreen.SetActive(false);
        Camera = GameObject.FindGameObjectWithTag("MainCamera");

        WatchingOutro = true;
        CameraSpeed = .05f;
        Camera.transform.position = IntroDestination.position; // start the camera at the beginning for now, maybe put something in later that doesn't play after the first time
        LoadOutroSequenec();
    }


    public void LoadOutroSequenec() // from settings or stats to menu
    {
        AtCreditsArea = true;

        CurrentDestination = CreditsDestination;
    }

    public void SkipOutro()
    {
        Skipping = true;
        WatchingOutro = false;
        Camera.transform.position = CurrentDestination.position;
        SkipCanvas.SetActive(false);
        LoadOutroSequenec();
    }

}

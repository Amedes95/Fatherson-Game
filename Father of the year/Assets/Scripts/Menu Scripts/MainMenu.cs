using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject MenuScreen;
    public GameObject SettingsMenu;
    public AudioSource SettingsButton;
    public GameObject ConfirmationMenu;
    public GameObject MusicSettingsMenu;
    public GameObject StatsMenu;
    public GameObject ControllerMenu;

    bool WipingProgress;
    bool AtMainMenu;
    bool ChoosingASetting; // used with the moving camera to toggle a canvas
    bool EditingSounds;
    bool EditingVisuals;
    bool EditingControls;
    bool WatchingIntro;
    bool BrowsingStats;

    GameObject Camera;
    Transform CurrentDestination; // this one gets set by the others

    public Transform SettingsDestination; // set the current destination with these
    public Transform MainMenuDestination;
    public Transform IntroDestination;
    public Transform SoundDestination;
    public Transform ControllerDestination;
    public Transform VisualsDestination;
    public Transform ProgressWipeDestination;
    public Transform StatsDestination;

    public float CameraSpeed;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (EditingSounds)
            {
                LoadSettings();
            }
            else if (WipingProgress)
            {
                DenyConfirmation();
            }
            else if (ChoosingASetting)
            {
                ExitSettings();
            }
            else if (BrowsingStats)
            {
                LoadMainMenu();
            }
            else if (EditingControls)
            {
                LoadSettings();
            }
        }


        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, CurrentDestination.position, CameraSpeed); // constantly move the camera to the "Current Destination"
        if (WatchingIntro && Input.GetKeyDown(KeyCode.Space)) // skips intro
        {
            WatchingIntro = false;
            Camera.transform.position = CurrentDestination.position;
        }
        if (Camera.transform.position == CurrentDestination.position) // we made it, decide from here
        {
            if (ChoosingASetting) // the destination was "settings menu"
            {
                SettingsMenu.SetActive(true);
            }
            else if (AtMainMenu) // "Main Menu"
            {
                MenuScreen.SetActive(true);
            }
            else if (EditingSounds)
            {
                MusicSettingsMenu.SetActive(true);
            }
            else if (EditingControls)
            {
                ControllerMenu.SetActive(true);
            }
            else if (EditingVisuals)
            {
                // tun on visuals canvas
            }
            else if (WipingProgress)
            {
                ConfirmationMenu.SetActive(true);
            }
            else if (BrowsingStats)
            {
                StatsMenu.SetActive(true);
            }
        }
    }

    public void LoadWorldHub() // Loads world hub scene
    {
        SceneManager.LoadScene("WorldHub");
    }

    public void ExitGame() // Closes the game (builds only)
    {
        Application.Quit();
    }

    private void Awake()
    {
        SettingsButton.playOnAwake = false;
        Time.timeScale = 1f;
    }

    public void Start()
    {
        MenuScreen.SetActive(false);
        Camera = GameObject.FindGameObjectWithTag("MainCamera");

        WatchingIntro = true;
        CameraSpeed = .05f;
        Camera.transform.position = IntroDestination.position; // start the camera at the beginning for now, maybe put something in later that doesn't play after the first time
        LoadMainMenu();
    }


    public void LoadSettings() // loads settings options
    {
        ChoosingASetting = true;

        AtMainMenu = false;
        EditingSounds = false;
        EditingControls = false;
        EditingVisuals = false;
        WipingProgress = false;

        CurrentDestination = SettingsDestination; // where does the camera go?
        CameraSpeed = .4f;

        MenuScreen.SetActive(false);
        MusicSettingsMenu.SetActive(false);
        ControllerMenu.SetActive(false);
        SettingsButton.playOnAwake = true;
    }

    public void ExitSettings() // settings to menu
    {
        ChoosingASetting = false;
        AtMainMenu = true;
        CurrentDestination = MainMenuDestination;
        CameraSpeed = .4f;

        SettingsMenu.SetActive(false);
    }

    public void AskConfirmation() // from settings to wiping progress screen
    {
        BrowsingStats = false;
        WipingProgress = true;

        CurrentDestination = ProgressWipeDestination;
        CameraSpeed = .5f;
        StatsMenu.SetActive(false);
    }

    public void DenyConfirmation() // from wipe progress confirmation screen to settings
    {
        WipingProgress = false;
        BrowsingStats = true;

        CurrentDestination = StatsDestination;
        CameraSpeed = .5f;
        ConfirmationMenu.SetActive(false);
    }

    public void ConfirmProgressWipe() // from progress wipe screen to settings
    {
        WipingProgress = false;
        BrowsingStats = true;
        CurrentDestination = StatsDestination;

        PlayerPrefs.DeleteAll();
        ConfirmationMenu.SetActive(false);
        MusicSettingsMenu.GetComponent<SoundManager>().RevertSFXVolume();
        MusicSettingsMenu.GetComponent<SoundManager>().RevertToDefault();

    }

    public void LoadSoundSettings() // from settings to sound settings
    {
        EditingSounds = true;
        ChoosingASetting = false;

        CurrentDestination = SoundDestination;
        CameraSpeed = .4f;
        SettingsMenu.SetActive(false);
    }

    public void LoadVisualSettings() // from settings to visual settings
    {
        EditingVisuals = true;
        ChoosingASetting = false;

        CurrentDestination = VisualsDestination;
        SettingsMenu.SetActive(false);
    }

    public void LoadControllerSettings() // from settings to controller settings
    {
        EditingControls = true;
        ChoosingASetting = false;

        CurrentDestination = ControllerDestination;
        SettingsMenu.SetActive(false);
    }

    public void LoadStatsMenu()
    {
        BrowsingStats = true;
        AtMainMenu = false;

        CurrentDestination = StatsDestination;
        CameraSpeed = .4f;
        MenuScreen.SetActive(false);
    } // from menu to stats screen

    public void LoadMainMenu() // from settings or stats to menu
    {
        AtMainMenu = true;
        BrowsingStats = false;
        ChoosingASetting = false;

        CurrentDestination = MainMenuDestination;
        SettingsMenu.SetActive(false);
        StatsMenu.SetActive(false);

    }

}

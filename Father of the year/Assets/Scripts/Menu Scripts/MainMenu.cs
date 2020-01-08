using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject MenuScreen;
    public GameObject SettingsMenu;
    public GameObject ConfirmationMenu;
    public AudioSource SettingsButton;
    public GameObject MusicSettingsMenu;

    bool WipingProgress;
    bool AtMainMenu;
    bool ChoosingASetting; // used with the moving camera to toggle a canvas
    bool EditingSounds;
    bool EditingVisuals;
    bool EditingControls;

    GameObject Camera;
    Transform CurrentDestination; // this one gets set by the others

    public Transform SettingsDestination; // set the current destination with these
    public Transform MainMenuDestination;
    public Transform IntroDestination;
    public Transform SoundDestination;
    public Transform ControllerDestination;
    public Transform VisualsDestination;
    public Transform ProgressWipeDestination;

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
        }

        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, CurrentDestination.position, .5f); // constantly move the camera to the "Current Destination"
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
                // turn on controls canvas
            }
            else if (EditingVisuals)
            {
                // tun on visuals canvas
            }
            else if (WipingProgress)
            {
                ConfirmationMenu.SetActive(true);
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
        MenuScreen.SetActive(true);
        Camera = GameObject.FindGameObjectWithTag("MainCamera");

        Camera.transform.position = IntroDestination.position; // start the camera at the beginning for now, maybe put something in later that doesn't play after the first time
        CurrentDestination = MainMenuDestination; // now go to menu
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

        MenuScreen.SetActive(false);
        SettingsMenu.SetActive(true);
        MusicSettingsMenu.SetActive(false);
        SettingsButton.playOnAwake = true;
    }

    public void ExitSettings() // settings to menu
    {
        ChoosingASetting = false;
        AtMainMenu = true;
        CurrentDestination = MainMenuDestination;

        SettingsMenu.SetActive(false);
    }

    public void AskConfirmation() // from settings to wiping progress screen
    {
        ChoosingASetting = false;
        WipingProgress = true;

        CurrentDestination = ProgressWipeDestination;
        SettingsMenu.SetActive(false);
    }

    public void DenyConfirmation() // from wipe progress confirmation screen to settings
    {
        WipingProgress = false;
        ChoosingASetting = true;

        CurrentDestination = SettingsDestination;
        ConfirmationMenu.SetActive(false);
    }

    public void ConfirmProgressWipe() // from progress wipe screen to settings
    {
        WipingProgress = false;
        ChoosingASetting = true;
        CurrentDestination = SettingsDestination;

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

}

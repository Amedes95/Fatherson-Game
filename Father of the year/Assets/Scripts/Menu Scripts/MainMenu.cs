using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public GameObject MenuScreen;
    public GameObject SettingsMenu;
    public AudioSource SettingsButton;
    public GameObject ConfirmationMenu;
    public GameObject MusicSettingsMenu;
    public GameObject StatsMenu;
    public GameObject ControllerMenu;
    public GameObject VisualsMenu;
    public GameObject SkipCanvas;
    public GameObject CostumeMenu;
    public GameObject AchievementMenu;
    public GameObject BonusGameMenu;

    bool WipingProgress;
    bool AtMainMenu;
    bool ChoosingASetting; // used with the moving camera to toggle a canvas
    bool EditingSounds;
    bool EditingVisuals;
    bool EditingControls;
    bool WatchingIntro;
    bool BrowsingStats;
    bool ChangingCostumes;
    bool BrowsingAchievements;
    bool BrowsingGames;

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
    public Transform CostumeDestination;
    public Transform AchievementDestination;
    public Transform BonusGamesDestination;

    public GameObject VeganConfirm;
    public GameObject MalConfirm;
    public GameObject BossConfirm;
    public StatsManager StatScreen;
    public UIControllerSupport BackButton;
    public Button MoreButton;
    public GameObject LockedImage;

    public float CameraSpeed;

    private void Update()
    {
        if (PlayerPrefs.GetInt("GameCompleted") == 1)
        {
            MoreButton.interactable = true;
            LockedImage.SetActive(false);
        }
        else // game not beaten
        {
            MoreButton.interactable = false;
            LockedImage.SetActive(true);
        }
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
            else if (EditingVisuals)
            {
                LoadSettings();
            }
            else if (ChangingCostumes)
            {
                LoadStatsMenu();
            }
            else if (BrowsingAchievements)
            {
                LoadStatsMenu();
            }
            else if (BrowsingGames)
            {
                if (BossConfirm.activeInHierarchy)
                {
                    StatScreen.DenyBossRush();
                    BackButton.FindFocus();
                }
                else if (MalConfirm.activeInHierarchy)
                {
                    StatScreen.DenyMalnourishedConfirmation();
                    BackButton.FindFocus();
                }
                else if (VeganConfirm.activeInHierarchy)
                {
                    StatScreen.DenyVeganConfirmation();
                    BackButton.FindFocus();
                }
                else
                {
                    LoadStatsMenu();
                }
            }
        }


        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, CurrentDestination.position, CameraSpeed); // constantly move the camera to the "Current Destination"
        if (WatchingIntro && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause")) && Camera.transform.position != CurrentDestination.position) // skips intro
        {
            SkipCanvas.SetActive(true);
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
                SkipCanvas.SetActive(false);
            }
            else if (EditingSounds) // sound menu
            {
                MusicSettingsMenu.SetActive(true);
            }
            else if (EditingControls) // controls menu
            {
                ControllerMenu.SetActive(true);
            }
            else if (EditingVisuals) // visuals menu
            {
                VisualsMenu.SetActive(true);
            }
            else if (WipingProgress) // wipe menu
            {
                ConfirmationMenu.SetActive(true);
            }
            else if (BrowsingStats) // stats menu
            {
                StatsMenu.SetActive(true);
            }
            else if (ChangingCostumes)
            {
                CostumeMenu.SetActive(true);
            }
            else if (BrowsingAchievements)
            {
                AchievementMenu.SetActive(true);
            }
            else if (BrowsingGames)
            {
                BonusGameMenu.SetActive(true);
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
        //PlayerPrefs.SetInt("PartyModeON", 0); // always turns party mode off upon exiting the game
        //PlayerPrefs.SetInt("OldTimeyON", 0); // always turn off old timey mode when quitting too
        PlayerPrefs.SetFloat("GameBegun", 0);
    }

    private void Awake()
    {
        PlayerPrefs.SetInt("Flawless Run", 1); // related to no deaths in a world achievement
        PlayerPrefs.SetInt("VeganMode", 0); // cancel vegan mode if cheating
        PlayerPrefs.SetFloat("VeganTimer", 0);
        PlayerPrefs.SetInt("Party Run", 0); // cancel party run, no cheating
        PlayerPrefs.SetInt("PartyModeON", 0);
        PlayerPrefs.SetInt("MalnourishedMode", 0); // cancel if quit, no cheaters
        PlayerPrefs.SetInt("MalnourishedLives", 0);
        PlayerPrefs.SetInt("BossRush", 0); // cancel boss rush, no cheaters


        SettingsButton.playOnAwake = false;
        Time.timeScale = 1f;

        VisualsScreen.Partying = false;
        VisualsScreen.BeingOld = false;
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
        VisualsMenu.SetActive(false);

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

        PlayerPrefs.SetFloat("GameBegun", 1);
        MusicSettingsMenu.GetComponentInParent<SoundManager>().RevertSFXVolume();
        MusicSettingsMenu.GetComponentInParent<SoundManager>().RevertToDefault();
        // helps change the music
        Boombox CurrentBoombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
        CurrentBoombox.UpdateSound();
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
        ChangingCostumes = false;
        BrowsingAchievements = false;
        AtMainMenu = false;
        BrowsingGames = false;

        CurrentDestination = StatsDestination;
        CameraSpeed = .4f;
        MenuScreen.SetActive(false);
        CostumeMenu.SetActive(false);
        AchievementMenu.SetActive(false);
        BonusGameMenu.SetActive(false);


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

    public void SkipIntro()
    {
        WatchingIntro = false;
        Camera.transform.position = CurrentDestination.position;
        SkipCanvas.SetActive(false);
        LoadMainMenu();
    }

    public void LoadCostumes()
    {
        ChangingCostumes = true;
        BrowsingStats = false;
        CurrentDestination = CostumeDestination;
        CameraSpeed = .4f;
        StatsMenu.SetActive(false);
    }

    public void LoadAchievements()
    {
        BrowsingAchievements = true;
        BrowsingStats = false;
        CurrentDestination = AchievementDestination;
        CameraSpeed = .4f;
        StatsMenu.SetActive(false);
    }

    public void LoadBonusGames()
    {
        BrowsingGames = true;
        BrowsingStats = false;
        CurrentDestination = BonusGamesDestination;
        CameraSpeed = .4f;
        StatsMenu.SetActive(false);
    }

}

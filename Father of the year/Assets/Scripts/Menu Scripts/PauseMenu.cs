using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject PauseScreen;
    public GameObject SettingsMenuChoices;
    public GameObject AudioSettings;
    public GameObject VisualSettings;
    public GameObject ControlsSettings;

    bool MakingSettingsChoice;
    bool EditingAudio;
    bool EditingVisuals;
    bool EditingControls;

    VictoryMenu VictoryScreen;
    GameObject Player;
    GameObject DeathScreen;
    GameObject Preloader;

    AudioSource PauseAudioSource;
    public AudioClip OpenNoise;
    public AudioClip CloseNoise;

    public PostProcessingProfile Transition1; // For film grain and black wipe effects
    public bool transitioning;
    public float ShadowValueUp;

    GameObject LevelManager;

    public GameObject ControllerConnected;
    public GameObject ControllerDisconnected;

    string PauseInput;

    public GameObject VeganModeDisplay;
    public TextMeshProUGUI VeganTimer;

    public GameObject MalnourishedDisplay;
    public TextMeshProUGUI MalnourishedLives;
    int MalLives;

    public GameObject BossRushDisplay;
    public GameObject RestartButton;
    public GameObject RestartButtonBoss;

    public bool SecretLevel;
    public string SecretLevelReturnWorld;

    string CancelInput;
    StandaloneInputModule Inputs;



    // Start is called before the first frame update
    void Awake()
    {
        PauseAudioSource = gameObject.GetComponent<AudioSource>();
        transitioning = false;
        GameIsPaused = false;
        DeathScreen = GameObject.FindGameObjectWithTag("DeathMenu");
        VictoryScreen = GameObject.FindGameObjectWithTag("VictoryMenu").GetComponent<VictoryMenu>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Preloader = GameObject.FindGameObjectWithTag("Preloader");
        var Blurry = Transition1.depthOfField.settings;
        Blurry.focalLength = 0f;
        Transition1.depthOfField.settings = Blurry;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().name == "WorldHub")
        {
            LevelManager = GameObject.FindGameObjectWithTag("LevelManager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("VeganMode") == 1)
        {
            VeganModeDisplay.SetActive(true);

            VeganTimer.text = "Time Passed: " + PlayerPrefs.GetFloat("VeganHours") + ":" + PlayerPrefs.GetString("VeganMinutes") + ":" + PlayerPrefs.GetString("VeganSeconds");
        }
        else
        {
            VeganModeDisplay.SetActive(false);
        }
        // malnourished pause display
        if (PlayerPrefs.GetInt("MalnourishedMode") == 1)
        {
            MalnourishedDisplay.SetActive(true);
            MalnourishedLives.text = PlayerPrefs.GetInt("MalnourishedLives").ToString();
        }
        else
        {
            MalnourishedDisplay.SetActive(false);
        }

        // Boss rush display
        if (PlayerPrefs.GetInt("BossRush") == 1)
        {
            BossRushDisplay.SetActive(true);

            RestartButton.SetActive(false);
            RestartButtonBoss.SetActive(true);
        }
        else
        {
            RestartButton.SetActive(true);
            RestartButtonBoss.SetActive(false);
            BossRushDisplay.SetActive(false);
        }


        if (Boombox.ControllerModeEnabled)
        {
            if (Boombox.PS4Enabled)
            {
                if (Application.platform != (RuntimePlatform.LinuxPlayer) && Application.platform != (RuntimePlatform.LinuxEditor)) // if not Linux, change controls
                {
                    PauseInput = "PS4Pause";

                    Inputs = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<StandaloneInputModule>();
                    Inputs.submitButton = "PS4Submit";
                    Inputs.cancelButton = "PS4Cancel";
                    CancelInput = "PS4Cancel";
                }
                else // If Linux
                {
                    Inputs = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<StandaloneInputModule>();
                    PauseInput = "Pause";
                    Inputs.cancelButton = "Cancel";
                    CancelInput = "Cancel";
                    Inputs.submitButton = "Submit";
                }

            }
            else
            {
                Inputs = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<StandaloneInputModule>();
                PauseInput = "Pause";
                Inputs.cancelButton = "Cancel";
                CancelInput = "Cancel";
                Inputs.submitButton = "Submit";
            }
        }
        else
        {
            PauseInput = "Escape";
            Inputs = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<StandaloneInputModule>();
            Inputs.cancelButton = "Cancel";
            Inputs.submitButton = "Submit";
            CancelInput = "Cancel";
        }

        if (Boombox.ControllerModeEnabled)
        {
            ControllerConnected.SetActive(true);
            ControllerDisconnected.SetActive(false);
        }
        else
        {
            ControllerConnected.SetActive(false);
            ControllerDisconnected.SetActive(true);
        }

        if (Input.GetButtonDown(Inputs.cancelButton.ToString()))
        {

            if (EditingAudio)
            {
                EditSettings();
                EditingAudio = false;
            }
            else if (EditingControls)
            {
                EditSettings();
                EditingControls = false;
            }
            else if (EditingVisuals)
            {
                EditSettings();
                EditingVisuals = false;
            }
            else if (MakingSettingsChoice)
            {
                LeaveSettings();
                MakingSettingsChoice = false;
            }
        }

        var Vinny = Transition1.vignette.settings; // black wipe effect

        if (Input.GetButtonDown(PauseInput) && VictoryScreen.GoalReached == false && Player.activeInHierarchy && transitioning == false)
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (transitioning)
        {
            Vinny.intensity += ShadowValueUp;
            if (Vinny.intensity >= 1)
            {
                if (SecretLevel)
                {
                    SceneManager.LoadScene(SecretLevelReturnWorld);
                }
                else
                {
                    SceneManager.LoadScene("WorldHub");
                }
            }
        }
        else
        {
            Vinny.intensity -= (ShadowValueUp + .01f);
            if (Vinny.intensity <= 0)
            {
                Vinny.intensity = 0;
            }
        }
        Transition1.vignette.settings = Vinny;
    }

    public void ResumeGame()
    {
        PauseScreen.SetActive(false);
        SettingsMenuChoices.SetActive(false);
        AudioSettings.SetActive(false);
        VisualSettings.SetActive(false);
        ControlsSettings.SetActive(false);
        Time.timeScale = 1f;
        Player.GetComponent<PlayerMovement>().enabled = true;
        if (SceneManager.GetActiveScene().name == "WorldHub")
        {
            LevelManager.GetComponent<LevelManager>().enabled = true;
            Player.GetComponent<PlayerMovement>().enabled = false;

        }

        GameIsPaused = false;

        DeathScreen.SetActive(true);
        VictoryScreen.gameObject.SetActive(true);
        var Blurry = Transition1.depthOfField.settings;
        Blurry.focalLength = 0f;
        Transition1.depthOfField.settings = Blurry;
        PauseAudioSource.clip = CloseNoise;
        PauseAudioSource.Play();

        MakingSettingsChoice = false;
        EditingAudio = false;
        EditingControls = false;
        EditingVisuals = false;

    }
    public void PauseGame()
    {
        if (!PlayerHealth.Dead)
        {
            if (Gamepad.current != null)
            {
                Gamepad.current.PauseHaptics();
            }
            PauseScreen.SetActive(true);
            Player.GetComponent<PlayerMovement>().enabled = false;
            if (SceneManager.GetActiveScene().name == "WorldHub")
            {
                LevelManager.GetComponent<LevelManager>().enabled = false;
            }
            Time.timeScale = 0f;
            GameIsPaused = true;
            DeathScreen.SetActive(false);
            VictoryScreen.gameObject.SetActive(false);
            var Blurry = Transition1.depthOfField.settings;
            Blurry.focalLength = 300f;
            Transition1.depthOfField.settings = Blurry;
            PauseAudioSource.clip = OpenNoise;
            PauseAudioSource.Play();
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        MalLives = PlayerPrefs.GetInt("MalnourishedLives");
        MalLives -= 1;
        if (PlayerPrefs.GetInt("MalnourishedMode") == 1 && MalLives > 0) // mal mode and lives remaining
        {
            PlayerPrefs.SetInt("MalnourishedLives", MalLives);
            Debug.Log("Malnourished lives" + PlayerPrefs.GetInt("MalnourishedLives"));
            if (MalLives <= 0)
            {
                QuitLevel();
            }
            else
            {
                VictoryScreen.ReloadScene();
            }
        }
        else if (PlayerPrefs.GetInt("MalnourishedMode") == 1 && MalLives <= 0) // mal mode and last life
        {
            QuitLevel();
        }
        else
        {
            if (PlayerPrefs.GetInt("BossRush") == 1)
            {
                QuitLevel();
            }
            else
            {
                VictoryScreen.ReloadScene();
            }
        }

    }

    public void QuitLevel()
    {
        Time.timeScale = 1f;
        transitioning = true;
        DeathScreen.SetActive(false);
        VictoryScreen.gameObject.SetActive(false);
        PauseScreen.SetActive(false);
        Preloader.SetActive(false);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Preloader.SetActive(false);
    }

    public void EditSettings() // Loads the choices of settings
    {
        MakingSettingsChoice = true;
        EditingAudio = false;
        EditingControls = false;
        EditingVisuals = false;

        SettingsMenuChoices.SetActive(true);
        AudioSettings.SetActive(false);
        VisualSettings.SetActive(false);
        ControlsSettings.SetActive(false);
        PauseScreen.SetActive(false);
    }
    public void LeaveSettings() // Clicking BACK BUTTON
    {
        SettingsMenuChoices.SetActive(false);
        AudioSettings.SetActive(false);
        PauseScreen.SetActive(true);
    }

    public void LoadAudioSettings() // Clicking AUDIO BUTTON
    {
        EditingAudio = true;

        SettingsMenuChoices.SetActive(false);
        AudioSettings.SetActive(true);
    }

    public void LoadControlsSettings() // Clicking CONTROLS BUTTON
    {
        EditingControls = true;

        SettingsMenuChoices.SetActive(false);
        ControlsSettings.SetActive(true);

    }

    public void LoadVisualSettings() // Clicking VISUAL BUTTON
    {
        EditingVisuals = true;

        SettingsMenuChoices.SetActive(false);
        VisualSettings.SetActive(true);
    }

}

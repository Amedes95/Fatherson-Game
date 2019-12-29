using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using UnityEngine.UIElements;
using TMPro;


public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused;
    public GameObject PauseScreen;
    public GameObject SettingsMenuChoices;
    public GameObject AudioSettings;
    public GameObject VisualSettings;
    public GameObject ControlsSettings;

    VictoryMenu VictoryScreen;
    GameObject Player;
    GameObject DeathScreen;
    GameObject Preloader;

    AudioSource PauseAudioSource;
    public AudioClip OpenNoise;
    public AudioClip CloseNoise;

    public PostProcessingProfile Transition1; // For film grain and black wipe effects
    bool transitioning;
    public float ShadowValueUp;




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
    }

    // Update is called once per frame
    void Update()
    {
        var Vinny = Transition1.vignette.settings; // black wipe effect

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause")) && VictoryScreen.GoalReached == false && Player.activeInHierarchy)
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
                SceneManager.LoadScene("WorldHub");
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
        GameIsPaused = false;
        DeathScreen.SetActive(true);
        VictoryScreen.gameObject.SetActive(true);
        var Blurry = Transition1.depthOfField.settings;
        Blurry.focalLength = 0f;
        Transition1.depthOfField.settings = Blurry;
        PauseAudioSource.clip = CloseNoise;
        PauseAudioSource.Play();

    }
    public void PauseGame()
    {
        if (!PlayerHealth.Dead)
        {
            PauseScreen.SetActive(true);
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
        VictoryScreen.ReloadScene();
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
        SettingsMenuChoices.SetActive(false);
        AudioSettings.SetActive(true);
    }

    public void LoadControlsSettings() // Clicking CONTROLS BUTTON
    {
        SettingsMenuChoices.SetActive(false);
        ControlsSettings.SetActive(true);
    }

    public void LoadVisualSettings() // Clicking VISUAL BUTTON
    {
        SettingsMenuChoices.SetActive(false);
        VisualSettings.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;


public class VictoryMenu : MonoBehaviour
{
    public string NextLevel;
    public bool GoalReached;
    public GameObject VictoryScreen;
    GameObject PauseCanvas;

    public float ShadowValueUp;
    public PostProcessingProfile Transition1; // Face in and out of black
    bool transitioning;
    bool LoadingWorldHub;

    public AudioSource StaticSource;
    public AudioClip StaticNoise;

    public GameObject SpaceBarText;

    public bool SpecialVictory;
    public GameObject NextButton;
    public GameObject ReplayButton;
    public GameObject ExitButton;
    public GameObject MalnourishedText;

    public GameObject RestartButtonBoss;


    private void Awake()
    {
        PauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
        LoadingWorldHub = false;

        // level transition vignette
        var Vinny = Transition1.vignette.settings;
        Vinny.intensity = 1f;
        transitioning = false;

        // chromatic abberation
        var Chroma = Transition1.chromaticAberration.settings;
        Chroma.intensity = 0;
        Transition1.chromaticAberration.settings = Chroma;

        // restart film grain
        var Grainy = Transition1.grain.settings;
        if (Grainy.intensity == 1f)
        {
            StaticSource.clip = StaticNoise;
            StaticSource.Play();
        }
    }

    private void Update()
    {
        if (Boombox.ControllerModeEnabled)
        {
            SpaceBarText.SetActive(false);
        }
        else
        {
            SpaceBarText.SetActive(true);
        }
        if (PlayerPrefs.GetInt("MalnourishedMode") == 1)
        {
            ReplayButton.SetActive(false);
            ExitButton.SetActive(false);
            MalnourishedText.SetActive(true);
        }
        if (PlayerPrefs.GetInt("BossRush") == 1)
        {
            ReplayButton.SetActive(false);
            RestartButtonBoss.SetActive(true);
        }
        if (PlayerPrefs.GetInt("VeganMode") == 1)
        {
            ReplayButton.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("VeganMode") == 0 && PlayerPrefs.GetInt("BossRush") == 0 && PlayerPrefs.GetInt("MalnourishedMode") == 0)
        {
            ReplayButton.SetActive(true);
            RestartButtonBoss.SetActive(false);
        }

        var Vinny = Transition1.vignette.settings;

        /// for film grain restart effect ///// 
        var Grainy = Transition1.grain.settings;
        Grainy.intensity -= .03f;
        if (Grainy.intensity <= 0f)
        {
            Grainy.intensity = 0f;
        }
        Transition1.grain.settings = Grainy;

        if (Grainy.intensity == 1f)
        {
            StaticSource.clip = StaticNoise;
            StaticSource.Play();
        }
        ////////////////////////////////////

        if (GoalReached)
        {
            VictoryScreen.SetActive(true);
            //NextButton.GetComponent<UIControllerSupport>().FindFocus();
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Pause") || Input.GetButtonDown("PS4Pause")) && SceneManager.GetActiveScene().name != "EndCredits")
            {
                LoadNextLevel();
            }
        }
        else
        {
            VictoryScreen.SetActive(false);
        }
        if (GoalReached && SpecialVictory)
        {
            ExitToHub();
        }

        // post processing transition
        if (transitioning)
        {
            VictoryScreen.SetActive(false);
            Vinny.intensity += ShadowValueUp;
            if (Vinny.intensity >= 1)
            {
                Vinny.intensity = 1;
                if (LoadingWorldHub)
                {
                    SceneManager.LoadScene("WorldHub");
                }
                else
                {
                    if (PlayerPrefs.GetInt("BossRush") == 1) // if boss rush is enabled
                    {
                        if (SceneManager.GetActiveScene().name == "W1BOSS")
                        {
                            SceneManager.LoadScene("W2BOSS");
                        }
                        else if (SceneManager.GetActiveScene().name == "W2BOSS")
                        {
                            SceneManager.LoadScene("W3BOSS");
                        }
                        else if (SceneManager.GetActiveScene().name == "W3BOSS")
                        {
                            SceneManager.LoadScene("W4BOSS");
                        }
                        else if (SceneManager.GetActiveScene().name == "W4BOSS")
                        {
                            SceneManager.LoadScene("W5BOSS");
                        }
                        else if (SceneManager.GetActiveScene().name == "W5BOSS")
                        {
                            SceneManager.LoadScene("W6BOSS");
                        }
                        else if (SceneManager.GetActiveScene().name == "W6BOSS")
                        {
                            SceneManager.LoadScene("EndCredits");
                        }
                    }
                    else // not boss rush
                    {
                        SceneManager.LoadScene(NextLevel);
                    }
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

    public void LoadNextLevel() // Next
    {
        transitioning = true;
        LoadingWorldHub = false;
        PauseCanvas.SetActive(false);
    }

    public void ExitToHub() // Quit
    {
        LoadingWorldHub = true;
        transitioning = true;
        if (!SpecialVictory)
        {
            PauseCanvas.SetActive(false);
        }
    }

    public void ReloadScene() // Restart
    {
        var Grainy = Transition1.grain.settings;
        Grainy.intensity = 1f;
        Transition1.grain.settings = Grainy;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

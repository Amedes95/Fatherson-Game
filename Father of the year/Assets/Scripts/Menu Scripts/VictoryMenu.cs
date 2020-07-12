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
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Pause"))
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
                    SceneManager.LoadScene(NextLevel);
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

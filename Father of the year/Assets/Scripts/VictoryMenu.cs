using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;


public class VictoryMenu : MonoBehaviour
{
    public string NextLevel;
    public bool LevelComplete;
    public GameObject VictoryScreen;

    public float ShadowValueUp;
    public PostProcessingProfile Transition1; // Face in and out of black
    bool transitioning;

    private void Awake()
    {
        var Vinny = Transition1.vignette.settings;
        Vinny.intensity = 1f;
        transitioning = false;
    }

    private void Update()
    {
        var Vinny = Transition1.vignette.settings;
        if (LevelComplete)
        {
            VictoryScreen.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadNextLevel();
            }
        }
        else
        {
            VictoryScreen.SetActive(false);
        }

        // post processing transition
        if (transitioning)
        {
            VictoryScreen.SetActive(false);
            Vinny.intensity += ShadowValueUp;
            if (Vinny.intensity >= 1)
            {
                Vinny.intensity = 1;
                SceneManager.LoadScene(NextLevel);
            }
        }
        else
        {
            Vinny.intensity -= (ShadowValueUp + .01f);
            if (Vinny.intensity <= 0)
            {
                Vinny.intensity = 0;
                ResumeGame();
            }
        }
        Transition1.vignette.settings = Vinny;
    }

    public void LoadNextLevel() // Next
    {
        transitioning = true;
    }

    public void ExitToHub() // Quit
    {
        SceneManager.LoadScene("WorldHub");
    }

    public void ReloadScene() // Restart
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}

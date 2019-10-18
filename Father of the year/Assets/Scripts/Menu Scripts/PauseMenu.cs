using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;


public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused;
    public GameObject PauseScreen;
    VictoryMenu VictoryScreen;
    GameObject Player;
    GameObject DeathScreen;

    public PostProcessingProfile Transition1; // For film grain and black wipe effects
    bool transitioning;
    public float ShadowValueUp;


    // Start is called before the first frame update
    void Awake()
    {
        transitioning = false;
        GameIsPaused = false;
        DeathScreen = GameObject.FindGameObjectWithTag("DeathMenu");
        VictoryScreen = GameObject.FindGameObjectWithTag("VictoryMenu").GetComponent<VictoryMenu>();
        Player = GameObject.FindGameObjectWithTag("Player");
        var Blurry = Transition1.depthOfField.settings;
        Blurry.focalLength = 0f;
        Transition1.depthOfField.settings = Blurry;

    }

    // Update is called once per frame
    void Update()
    {
        var Vinny = Transition1.vignette.settings; // black wipe effect

        if (Input.GetKeyDown(KeyCode.Escape) && VictoryScreen.LevelComplete == false && Player.activeInHierarchy)
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
        Time.timeScale = 1f;
        GameIsPaused = false;
        DeathScreen.SetActive(true);
        VictoryScreen.gameObject.SetActive(true);
        var Blurry = Transition1.depthOfField.settings;
        Blurry.focalLength = 0f;
        Transition1.depthOfField.settings = Blurry;

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

    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

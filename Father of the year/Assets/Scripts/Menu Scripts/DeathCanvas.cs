using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using UnityEngine.UIElements;
using TMPro;


public class DeathCanvas : MonoBehaviour
{
    public GameObject DeathMenu; // I reference this object instead of the actual canvas because scripts don't run when they're disabled
    public GameObject RespawnButton;
    bool transitioning;
    public float ShadowValueUp;
    GameObject VictoryMenu;
    GameObject PauseCanvas;

    public GameObject SpaceBarText;
    public GameObject PressStartText;

    public GameObject MalnourishedDisplay;
    public TextMeshProUGUI DeathCountDisplay;
    public GameObject MalnourishedButton;

    public TextMeshProUGUI RespawnText;


    public PostProcessingProfile Transition1; // For film grain

    private void Awake()
    {
        PauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
        VictoryMenu = GameObject.FindGameObjectWithTag("VictoryMenu");
        transitioning = false;
    }

    void Update()
    {
        if (Boombox.ControllerModeEnabled)
        {
            SpaceBarText.SetActive(false);
            PressStartText.SetActive(true);
        }
        else
        {
            SpaceBarText.SetActive(true);
            PressStartText.SetActive(false);
        }
        if (PlayerPrefs.GetInt("MalnourishedMode") == 1)
        {
            MalnourishedDisplay.SetActive(true);
            DeathCountDisplay.text = PlayerPrefs.GetInt("MalnourishedLives").ToString();
            if (PlayerPrefs.GetInt("MalnourishedLives") <= 0)
            {
                MalnourishedButton.SetActive(true);
            }
        }
        else
        {
            MalnourishedDisplay.SetActive(false);
        }




        var Vinny = Transition1.vignette.settings;

        //// film grain stuff ////////
        var Grainy = Transition1.grain.settings;
        Grainy.intensity -= .03f;
        if (Grainy.intensity <= 0f)
        {
            Grainy.intensity = 0f;
        }
        Transition1.grain.settings = Grainy;
        //////////////////



        // activated death screen when player dies
        if (PlayerHealth.Dead)
        {
            if (!transitioning)
            {
                DeathMenu.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Pause")) // make them wait...
            {
                if (PlayerPrefs.GetInt("MalnourishedMode") == 1 && PlayerPrefs.GetInt("MalnourishedLives") > 0)
                {
                    ReloadScene();
                }
                else if (PlayerPrefs.GetInt("BossRush") == 1) // if you try to restart boss rush, start the mode over
                {
                    SceneManager.LoadScene("W1BOSS");
                }
                else if (PlayerPrefs.GetInt("MalnourishedMode") == 0 && PlayerPrefs.GetInt("BossRush") == 0)
                {
                    ReloadScene();
                }
            }
            if (PlayerPrefs.GetInt("BossRush") == 1)
            {
                RespawnText.text = " Try Again?";
            }
            else
            {
                RespawnText.text = "Respawn";
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
        else
        {
            DeathMenu.SetActive(false);
        }

    }


    // this function is called when the player clicks respawn
    public void ReloadScene()
    {
        var Grainy = Transition1.grain.settings;
        Grainy.intensity = 1f;
        Transition1.grain.settings = Grainy;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GiveUp()
    {
        transitioning = true;
        VictoryMenu.SetActive(false);
        DeathMenu.SetActive(false);
        PauseCanvas.SetActive(false);
    }

}

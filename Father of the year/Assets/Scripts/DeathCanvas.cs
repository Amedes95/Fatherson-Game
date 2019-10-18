using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;


public class DeathCanvas : MonoBehaviour
{
    public GameObject DeathMenu; // I reference this object instead of the actual canvas because scripts don't run when they're disabled
    public GameObject RespawnButton;
    bool transitioning;
    public float ShadowValueUp;
    GameObject VictoryMenu;

    public PostProcessingProfile Transition1; // For film grain

    private void Awake()
    {
        VictoryMenu = GameObject.FindGameObjectWithTag("VictoryMenu");
        transitioning = false;
    }

    void Update()
    {
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
            if (Input.GetKeyDown(KeyCode.Space) && RespawnButton.activeInHierarchy) // make them wait...
            {
                ReloadScene();
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
        Debug.Log("Quitter");
    }

}

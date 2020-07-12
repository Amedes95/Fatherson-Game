using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CreditsManager : MonoBehaviour
{
    public GameObject CreditsScreen;
    public GameObject SkipCanvas;

    bool AtCreditsArea;
    bool WatchingOutro;

    GameObject Camera;
    Transform CurrentDestination; // this one gets set by the others

    public Transform CreditsDestination;
    public Transform IntroDestination;

    public float CameraSpeed;

    public GameObject Credits;
    bool Skipping;

    private void Update()
    {
        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, CurrentDestination.position, CameraSpeed); // constantly move the camera to the "Current Destination"
        if (WatchingOutro && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Pause")) && Camera.transform.position != CurrentDestination.position) // skips intro
        {
            SkipCanvas.SetActive(true);
        }
        if (Camera.transform.position == CurrentDestination.position) // we made it, decide from here
        {
            if (AtCreditsArea) // "Credits Menu"
            {
                CreditsScreen.SetActive(true);
                //SkipCanvas.SetActive(false);
                Credits.SetActive(true);
            }
        }
        if (Skipping)
        {
            Credits.GetComponent<Animator>().SetBool("Skip", true);
        }
    }

    public void LoadWorldHub() // Loads world hub scene
    {
        SceneManager.LoadScene("WorldHub");
    }

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void Start()
    {
        CreditsScreen.SetActive(false);
        Camera = GameObject.FindGameObjectWithTag("MainCamera");

        WatchingOutro = true;
        CameraSpeed = .05f;
        Camera.transform.position = IntroDestination.position; // start the camera at the beginning for now, maybe put something in later that doesn't play after the first time
        LoadOutroSequenec();
    }


    public void LoadOutroSequenec() // from settings or stats to menu
    {
        AtCreditsArea = true;

        CurrentDestination = CreditsDestination;
    }

    public void SkipOutro()
    {
        Skipping = true;
        WatchingOutro = false;
        Camera.transform.position = CurrentDestination.position;
        SkipCanvas.SetActive(false);
        LoadOutroSequenec();
    }

}

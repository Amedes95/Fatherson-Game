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
    public GameObject GoalParent;

    private void Update()
    {
        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, CurrentDestination.position, CameraSpeed); // constantly move the camera to the "Current Destination"
        if (WatchingOutro && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause")) && Camera.transform.position != CurrentDestination.position && GoalParent.transform.localPosition.y != 9.5) // skips intro
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
        if (GoalParent.transform.localPosition.y == 9.5)
        {
            SkipCanvas.SetActive(false);
        }
    }

    public void LoadWorldHub() // Loads world hub scene
    {
        SceneManager.LoadScene("WorldHub");
    }

    private void Awake()
    {
        Time.timeScale = 1f;
        /// Unlocks Carnist Achievement
        if (PlayerPrefs.GetInt("Carnist") == 0 && PlayerPrefs.GetInt("VeganMode") == 1)
        {
            PlayerPrefs.SetInt("Carnist", 1);
            Debug.Log("Carnist Unlocked");
            Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
            Boombox.UnlockCheevo("Carnist");
        }

        if (PlayerPrefs.GetFloat("VeganTimer") <= 7200 && PlayerPrefs.GetInt("VeganMode") == 1) // if credits are reached under 2 hours
        {
            /// Unlocks Fast Food Achievement
            if (PlayerPrefs.GetInt("Fast Food") == 0)
            {
                PlayerPrefs.SetInt("Fast Food", 1);
                Debug.Log("Fast Food Unlocked");
                Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                Boombox.UnlockCheevo("Fast Food");
            }
        }
        if (PlayerPrefs.GetFloat("Insatiable Appetite") <= 7200 && PlayerPrefs.GetInt("MalnourishedMode") == 1) // if credits are reached during malnourished mode
        {
            /// Unlocks Insatiable Appetite Achievement
            if (PlayerPrefs.GetInt("Insatiable Appetite") == 0)
            {
                PlayerPrefs.SetInt("Insatiable Appetite", 1);
                Debug.Log("Insatiable Appetite");
                Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                Boombox.UnlockCheevo("Insatiable Appetite");
            }
        }
        if (PlayerPrefs.GetInt("BossRush") == 1)
        {
            /// Unlocks Indigestible Achievement
            if (PlayerPrefs.GetInt("Indigestible") == 0)
            {
                PlayerPrefs.SetInt("Indigestible", 1);
                Debug.Log("Indigestible");
                Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                Boombox.UnlockCheevo("Indigestible");
            }
        }

        PlayerPrefs.SetInt("VeganMode", 0); // stops vegan mode
        PlayerPrefs.SetInt("MalnourishedMode", 0); // stop malnourished mode
        PlayerPrefs.SetInt("BosRush", 0); // stop bosh rush mode

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

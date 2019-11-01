using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;


public class Goal : MonoBehaviour
{
    VictoryMenu VictoryScreen;
    public PostProcessingProfile Transition1;
    bool PulsingChroma;
    bool Rising;
    float BestTime;



    bool SpeedRunning;
    public float CompletionTime;

    // Start is called before the first frame update
    void Awake()
    {
        BestTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name);
        SpeedRunning = true;
        CompletionTime = 0f;
        PulsingChroma = false;
        VictoryScreen = GameObject.FindGameObjectWithTag("VictoryMenu").GetComponent<VictoryMenu>();
        VictoryScreen.LevelComplete = false;
        var Chroma = Transition1.chromaticAberration.settings;
        Chroma.intensity = 0;
        Transition1.chromaticAberration.settings = Chroma;
    }


    private void OnTriggerEnter2D(Collider2D collision) // player completed level
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Collector>().AddToFruitStash();
            collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.GetComponent<Animator>().SetTrigger("Complete");
            collision.gameObject.SetActive(false);

            ////// Compare best time with completion time for records
            SpeedRunning = false; // stop timer
            if (PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name) == 0f) // if its your first run
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, (float) CompletionTime); // update playerprefs with your best time!
                Debug.Log("First timer" + PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name));
            }
            else if (PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name) != 0f) // did you do better?
            {
                if (CompletionTime < BestTime)
                {
                    PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, (float)CompletionTime); // update playerprefs with your new best time!
                    Debug.Log("New record" + PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name));
                }

            }


        }
    }

    public void DisplayVictoryScreen()
    {
        VictoryScreen.LevelComplete = true;
        var Chroma = Transition1.chromaticAberration.settings;
        Chroma.intensity = 1;
        Transition1.chromaticAberration.settings = Chroma;
        PulsingChroma = true;
    }

    private void Update()
    {
        var Chroma = Transition1.chromaticAberration.settings;
        if (PulsingChroma)
        {
            if (Chroma.intensity == 1f)
            {
                Rising = false;
            }
            else if (Chroma.intensity == 0f)
            {
                Rising = true;
            }

            if (Rising)
            {
                Chroma.intensity += .02f;
                Transition1.chromaticAberration.settings = Chroma;
                if (Chroma.intensity >= 1f)
                {
                    Chroma.intensity = 1f;
                    Rising = false;
                }
            }
            else if (Rising == false)
            {
                Chroma.intensity -= .02f;
                Transition1.chromaticAberration.settings = Chroma;
                if (Chroma.intensity <= 0f)
                {
                    Chroma.intensity = 0f;
                    Rising = true;
                }
            }




        }

    }

    private void FixedUpdate()
    {
        if (SpeedRunning)
        {
            CompletionTime += Time.smoothDeltaTime;
        }
    }
}

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


    // Start is called before the first frame update
    void Awake()
    {
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
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1); // level beaten
            collision.GetComponent<Collector>().AddToFruitStash();
            collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.GetComponent<Animator>().SetTrigger("Complete");
            collision.gameObject.SetActive(false);
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

    private void FixedUpdate()
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
}

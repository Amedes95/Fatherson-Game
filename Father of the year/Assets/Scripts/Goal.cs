using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;


public class Goal : MonoBehaviour
{
    VictoryMenu VictoryScreen;
    public PostProcessingProfile Transition1;

    // Start is called before the first frame update
    void Awake()
    {
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
    }
}

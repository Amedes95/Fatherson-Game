using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    VictoryMenu VictoryScreen;

    // Start is called before the first frame update
    void Awake()
    {
        VictoryScreen = GameObject.FindGameObjectWithTag("VictoryMenu").GetComponent<VictoryMenu>();
        VictoryScreen.LevelComplete = false;
    }


    private void OnTriggerEnter2D(Collider2D collision) // player completed level
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1); // level beaten

            collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            VictoryScreen.LevelComplete = true;
            Debug.Log(SceneManager.GetActiveScene().name);
        }
    }
}

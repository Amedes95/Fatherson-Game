using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalLevel : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name == "Tutorial_20") // if its the last level of the tutorial
            {
                PlayerPrefs.SetInt("Tutorial_Complete", 1);
            }
            if (SceneManager.GetActiveScene().name == "W1BOSS") // w1 boss beaten
            {
                PlayerPrefs.SetInt("World1_Complete", 1);
            }
            if (SceneManager.GetActiveScene().name == "W2BOSS") // w2 boss beaten
            {
                PlayerPrefs.SetInt("World2_Complete", 1);
            }
            if (SceneManager.GetActiveScene().name == "W3BOSS") // w3 boss beaten
            {
                PlayerPrefs.SetInt("World3_Complete", 1);
            }
            if (SceneManager.GetActiveScene().name == "W4BOSS") // w4 boss beaten
            {
                PlayerPrefs.SetInt("World4_Complete", 1);
            }
            if (SceneManager.GetActiveScene().name == "W5BOSS") // w5 boss beaten
            {
                PlayerPrefs.SetInt("World5_Complete", 1);
            }
        }
    }
}

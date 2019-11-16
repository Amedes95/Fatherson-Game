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
            if (SceneManager.GetActiveScene().name == "Tutorial_20") // if its the last level
            {
                Debug.Log("Final Level Complete!");
                PlayerPrefs.SetInt("Tutorial_Complete", 1);
            }
            if (SceneManager.GetActiveScene().name == "Insert Name of world 1 final level")
            {
                PlayerPrefs.SetInt("Final World 1 level dad adda dad  ...and so on", 1);
            }
        }
    }
}

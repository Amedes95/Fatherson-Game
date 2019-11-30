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
            if (SceneManager.GetActiveScene().name == "Tutorial_19") // if its the last level
            {
                PlayerPrefs.SetInt("Tutorial_Complete", 1);
            }
            if (SceneManager.GetActiveScene().name == "W1L22")
            {
                PlayerPrefs.SetInt("World1_Complete", 1);
            }
        }
    }
}

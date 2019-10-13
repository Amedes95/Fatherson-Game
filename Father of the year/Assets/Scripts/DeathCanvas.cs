using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // I added this to allow new functions


public class DeathCanvas : MonoBehaviour
{
    public GameObject DeathMenu; // I reference this object instead of the actual canvas because scripts don't run when they're disabled

    void Update()
    {
        // activated death screen when player dies
        if (PlayerHealth.Dead)
        {
            DeathMenu.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ReloadScene();
            }
        }
        else
        {
            DeathMenu.SetActive(false);
        }
    }


    // this function is called when the player clicks respawn
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public string NextLevel;
    public bool LevelComplete;
    public GameObject VictoryScreen;


    private void Update()
    {
        if (LevelComplete)
        {
            VictoryScreen.SetActive(true);
        }
        else
        {
            VictoryScreen.SetActive(false);
        }
    }

    public void LoadNextLevel() // Next
    {
        SceneManager.LoadScene(NextLevel);
    }

    public void ExitToHub() // Quit
    {
        SceneManager.LoadScene("WorldHub");
    }

    public void ReloadScene() // Restart
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

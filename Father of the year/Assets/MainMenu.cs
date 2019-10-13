using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public void LoadWorldHub()
    {
        SceneManager.LoadScene("WorldHub");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadSettings()
    {
        // Swap the UI on the main menu canvas with a settings canvas
    }
}

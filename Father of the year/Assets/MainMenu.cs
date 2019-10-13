using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public GameObject MenuScreen;
    public GameObject SettingsMenu;

    public void LoadWorldHub() // Loads world hub scene
    {
        SceneManager.LoadScene("WorldHub");
    }

    public void ExitGame() // Closes the game (builds only)
    {
        Application.Quit();
    }

    public void LoadSettings() // from menu to settings
    {
        MenuScreen.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void ExitSettings() // settings to menu
    {
        MenuScreen.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void WipeProgress()
    {
        PlayerPrefs.DeleteAll();
    }

}

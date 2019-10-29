using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public GameObject MenuScreen;
    public GameObject SettingsMenu;
    public GameObject ConfirmationMenu;
    public GameObject TitleText;

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

    public void AskConfirmation()
    {
        SettingsMenu.SetActive(false);
        ConfirmationMenu.SetActive(true);
        TitleText.SetActive(false);
    }

    public void DenyConfirmation()
    {
        ConfirmationMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        TitleText.SetActive(true);

    }

    public void ConfirmProgressWipe()
    {
        PlayerPrefs.DeleteAll();
        ConfirmationMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        TitleText.SetActive(true);

    }


}

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
    public AudioSource SettingsButton;
    public GameObject MusicSettingsMenu;

    bool EditingSounds;
    bool WipingProgress;
    bool ChoosingASetting;


    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (EditingSounds)
            {
                LoadSettings();
            }
            else if (WipingProgress)
            {
                DenyConfirmation();
            }
            else if (ChoosingASetting)
            {
                ExitSettings();
            }
        }
    }

    public void LoadWorldHub() // Loads world hub scene
    {
        SceneManager.LoadScene("WorldHub");
    }

    public void ExitGame() // Closes the game (builds only)
    {
        Application.Quit();
    }

    private void Awake()
    {
        SettingsButton.playOnAwake = false;
        Time.timeScale = 1f;
    }

    public void LoadSettings() // from menu to settings
    {
        ChoosingASetting = true;
        EditingSounds = false;
        WipingProgress = false;


        MenuScreen.SetActive(false);
        SettingsMenu.SetActive(true);
        MusicSettingsMenu.SetActive(false);
        SettingsButton.playOnAwake = true;
        TitleText.SetActive(false);
    }

    public void ExitSettings() // settings to menu
    {
        ChoosingASetting = false;

        MenuScreen.SetActive(true);
        SettingsMenu.SetActive(false);
        TitleText.SetActive(true);
    }

    public void AskConfirmation()
    {
        WipingProgress = true;

        SettingsMenu.SetActive(false);
        ConfirmationMenu.SetActive(true);
        TitleText.SetActive(false);
    }

    public void DenyConfirmation()
    {
        WipingProgress = false;

        ConfirmationMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void ConfirmProgressWipe()
    {
        WipingProgress = false;

        PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetFloat("GameBegun", 1);
        ConfirmationMenu.SetActive(false);
        LoadMusicSettings();
        LoadSettings();
        ExitSettings();
        MusicSettingsMenu.GetComponent<SoundManager>().RevertSFXVolume();
        MusicSettingsMenu.GetComponent<SoundManager>().RevertToDefault();

    }

    public void LoadMusicSettings()
    {
        EditingSounds = true;

        SettingsMenu.SetActive(false);
        MusicSettingsMenu.SetActive(true);
        TitleText.SetActive(false);
    }


}

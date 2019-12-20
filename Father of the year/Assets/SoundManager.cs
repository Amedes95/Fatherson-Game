using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    public GameObject MusicVolume;
    public GameObject SFXVolume;
    Slider MusicSlider;
    Slider SFXSlider;
    float DefaultMusicVolume;
    float DefaultSFXVolume = .8f;
    public AudioSource BGM;
    float MinorDelay = .1f;
    bool ReadyToGo;

    private void FixedUpdate()
    {
        MinorDelay -= Time.deltaTime;
        if (MinorDelay <= 0)
        {
            MinorDelay = 0;
            if (ReadyToGo == false)
            {
                InitializeSound();
                ReadyToGo = true;
            }
        }
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            InitializeSound();
            ReadyToGo = true;
        }
    }


    public void InitializeSound()
    {
        BGM = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<AudioSource>();
        DefaultMusicVolume = BGM.volume;

        MusicSlider = MusicVolume.GetComponent<Slider>();

        SFXSlider = SFXVolume.GetComponent<Slider>();
        if (PlayerPrefs.GetFloat("GameBegun") == 0)
        {
            PlayerPrefs.SetFloat("GameBegun", 1);
            MusicSlider.value = DefaultMusicVolume;
            SFXSlider.value = DefaultSFXVolume;
        }
        else
        {
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
    }

    public void RevertToDefault()
    {
        BGM.volume = DefaultMusicVolume;
        PlayerPrefs.SetFloat("MusicVolume", DefaultMusicVolume); // updates preferences with change
        MusicSlider.value = DefaultMusicVolume;
    }

    public void EditVolume()
    {
        if (Boombox.EditorMode == false)
        {
            BGM.volume = MusicSlider.value;
            PlayerPrefs.SetFloat("MusicVolume", BGM.volume); // updates preferences with change
        }

    }

    public void RevertSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVolume", DefaultSFXVolume);
        SFXSlider.value = DefaultSFXVolume;
    }

    public void EditSFXVolume()
    {
        if (Boombox.EditorMode == false)
        {
            PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
        }
    }
}

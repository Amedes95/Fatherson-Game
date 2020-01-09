using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class SoundManager : MonoBehaviour
{

    public GameObject MusicVolume;
    public GameObject SFXVolume;
    Slider MusicSlider;
    Slider SFXSlider;
    float DefaultMusicVolume = .4f;
    float DefaultSFXVolume = .6f;
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

    private void Start()
    {
        InitializeSound();
        ReadyToGo = true;
    }


    public void InitializeSound()
    {
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
        InitializeSound();
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
        InitializeSound();
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

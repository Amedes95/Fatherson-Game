using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public GameObject MusicVolume;
    public GameObject SFXVolume;
    Slider MusicSlider;
    Slider SFXSlider;
    float DefaultMusicVolume;
    float DefaultSFXVolume = 1f;
    public AudioSource BGM;


    private void Update()
    {
        BGM = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Awake()
    {
        BGM = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<AudioSource>();
        DefaultMusicVolume = BGM.volume;

        MusicSlider = MusicVolume.GetComponent<Slider>();

        SFXSlider = SFXVolume.GetComponent<Slider>();
        if (PlayerPrefs.GetFloat("GameBegun") == 0)
        {
            PlayerPrefs.SetFloat("GameBegun", 1);
            MusicSlider.value = DefaultMusicVolume;
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
    }

    public void EditVolume()
    {
        BGM.volume = MusicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", BGM.volume); // updates preferences with change
    }

    public void RevertSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVolume", DefaultSFXVolume);
    }

    public void EditSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
    }
}

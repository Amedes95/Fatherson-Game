using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    AudioSource SFXSource;

    // Start is called before the first frame update
    void Awake()
    {
        SFXSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        SFXSource.volume = PlayerPrefs.GetFloat("SFXVolume");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class UISound : MonoBehaviour
{
    AudioSource ButtonSource;
    public AudioClip Highlighted;
    public AudioClip Clicked;

    private void Awake()
    {
        ButtonSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayTickNoise()
    {
        ButtonSource.clip = Highlighted;
        if (gameObject.activeInHierarchy)
        {
            ButtonSource.Play();
        }
    }

    public void PlayCLickNoise()
    {
        ButtonSource.clip = Clicked;
        if (gameObject.activeInHierarchy)
        {
            ButtonSource.Play();
        }
    }
}

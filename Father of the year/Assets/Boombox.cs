using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boombox : MonoBehaviour
{
    public AudioClip LevelMusic;
    BackgroundMusic BGMusic;

    // Start is called before the first frame update
    void Awake()
    {
        BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
        BGMusic.CompareSongs();
    }
}

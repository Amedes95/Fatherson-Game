using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    AudioSource GameMusicPlayer;
    public AudioClip LevelMusic;

    // Start is called before the first frame update
    void Awake()
    {
        GameMusicPlayer = gameObject.GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);

        GameMusicPlayer.clip = LevelMusic;
        GameMusicPlayer.Play();

        CompareSongs();

    }

    public void CompareSongs()
    {
        if (LevelMusic == GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>().LevelMusic)
        {
            Debug.Log("TheSame");
        }
        else
        {
            Debug.Log("NotTheSame");
            LevelMusic = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>().LevelMusic;
            GameMusicPlayer.clip = LevelMusic;
            GameMusicPlayer.Play();
        }
    }

}

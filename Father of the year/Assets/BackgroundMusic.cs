using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic MusicControl;
    //public GameObject BGMusicPrefab;
    AudioSource GameMusicPlayer;
    public AudioClip LevelMusic;
    public static bool PauseActive;

    // Start is called before the first frame update
    void Awake()
    {
        GameMusicPlayer = gameObject.GetComponent<AudioSource>();

        if (MusicControl == null)
        {
            DontDestroyOnLoad(gameObject);
            MusicControl = this;
            GameMusicPlayer.clip = LevelMusic;
            GameMusicPlayer.Play();
            CompareSongs();
        }
        else if (MusicControl != this)
        {
            Destroy(gameObject);
        }

    }

    public void CompareSongs()
    {
        if (LevelMusic == GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>().LevelMusic)
        {
            //Debug.Log("TheSame");
        }
        else
        {
            //Debug.Log("NotTheSame");
            LevelMusic = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>().LevelMusic;
            GameMusicPlayer.clip = LevelMusic;
            GameMusicPlayer.Play();
        }
    }

    private void Update()
    {
        GameMusicPlayer.volume = PlayerPrefs.GetFloat("MusicVolume"); // saves those settings baby





        //// old code that made the music dampen while paused
        //if (PauseActive)
        //{
        //    GameMusicPlayer.volume = GameMusicPlayer.volume = .1f;
        //}
        //else
        //{
        //    if (SceneManager.GetActiveScene().name != "MainMenu")
        //    {
        //        GameMusicPlayer.volume = GameMusicPlayer.volume = .5f;
        //    }
        //}
    }
}

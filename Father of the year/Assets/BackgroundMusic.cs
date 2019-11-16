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
        }
        else if (MusicControl != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);


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

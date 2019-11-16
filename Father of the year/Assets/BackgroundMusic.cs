using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic control;

    public static AudioSource GameMusicPlayer;
    public AudioClip TutorialMusic;
    public AudioClip MenuMusic;
    public AudioClip WorldHubMusic;

    public AudioClip CurrentMusic;

    AudioClip LevelMusic;



    // Start is called before the first frame update
    void Awake()
    {
        GameMusicPlayer = gameObject.GetComponent<AudioSource>();
        LevelMusic = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>().LevelMusic;

        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

        GameMusicPlayer.clip = CurrentMusic;
        GameMusicPlayer.Play();

    }

    private void Update()
    {

    }


    private void OnLevelWasLoaded(int level)
    {
        if (CurrentMusic != LevelMusic)
        {
            GameMusicPlayer.clip = LevelMusic;
            GameMusicPlayer.Play();
        }
    }

}

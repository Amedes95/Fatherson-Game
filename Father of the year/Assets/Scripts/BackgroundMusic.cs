using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Steamworks;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic MusicControl;
    //public GameObject BGMusicPrefab;
    AudioSource GameMusicPlayer;
    public AudioClip LevelMusic;
    public TextMeshProUGUI CheevoText;


    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetFloat("RumbleToggled", 1);

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
        if (LevelMusic == GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>().LevelMusic) // if the music I am currently playing is the same as the one in the level, do nothing
        {
            //Debug.Log("TheSame");
        }
        else // if the music I am playing is different than the one supposed to be playing in the level
        {
            LevelMusic = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>().LevelMusic; // take my music and play the level music instead
            GameMusicPlayer.clip = LevelMusic;
            GameMusicPlayer.Play();
        }
    }

    private void Update()
    {
        GameMusicPlayer.volume = PlayerPrefs.GetFloat("MusicVolume"); // saves those settings baby
    }

    public void UnlockCheevo(string CheevoName)
    {
        CheevoText.text = "Achievement Unlocked: " + CheevoName;
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Unlock");
        if (SteamManager.Initialized) // initialized steam only
        {
            UnlockSteamCheevo(CheevoName);
        }
    }

    public void UnlockSteamCheevo(string SteamName)
    {
        SteamUserStats.SetAchievement(SteamName);
        SteamUserStats.StoreStats();
        Debug.Log("Steam cheevo unlocked");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boombox : MonoBehaviour
{
    public AudioClip LevelMusic;
    BackgroundMusic BGMusic;
    public GameObject BGPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        if (GameObject.FindGameObjectWithTag("BGMusic") == null)
        {
            Instantiate(BGPrefab);
            BGPrefab.GetComponent<BackgroundMusic>().LevelMusic = LevelMusic;
            BGPrefab.GetComponent<BackgroundMusic>().CompareSongs();
        }
        else
        {
            BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
            BGMusic.CompareSongs();
        }

    }
}

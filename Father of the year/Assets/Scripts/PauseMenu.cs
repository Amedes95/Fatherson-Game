using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject PauseScreen;
    VictoryMenu VictoryScreen;


    // Start is called before the first frame update
    void Awake()
    {
        VictoryScreen = GameObject.FindGameObjectWithTag("VictoryMenu").GetComponent<VictoryMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && VictoryScreen.LevelComplete == false)
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }
    public void PauseGame()
    {
        if (!PlayerHealth.Dead)
        {
            PauseScreen.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }



    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused;
    public GameObject PauseScreen;
    VictoryMenu VictoryScreen;
    GameObject Player;


    // Start is called before the first frame update
    void Awake()
    {
        GameIsPaused = false;
        VictoryScreen = GameObject.FindGameObjectWithTag("VictoryMenu").GetComponent<VictoryMenu>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && VictoryScreen.LevelComplete == false && Player.activeInHierarchy)
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

    public void Restart()
    {
        Time.timeScale = 1f;
        VictoryScreen.ReloadScene();
    }

    public void QuitLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

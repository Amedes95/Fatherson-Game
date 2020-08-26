using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public LevelManager LevelManager;
    public PauseMenu PauseMenu;
    public GameObject DebugCanvas;
    bool BuggerActive;


    public void UnlockAllLevels()
    {

        LevelManager.UnlockAllWorlds();

        foreach (GameObject World in LevelManager.WorldsList)
        {
            ListofLevels CurrentWorld = World.GetComponent<ListofLevels>();
            foreach (GameObject Level in CurrentWorld.LevelsWithinWorld)
            {
                string LevelID = Level.GetComponent<LevelInfo>().SceneToLoad;
                PlayerPrefs.SetFloat(LevelID, 260);
            }
        }
        PauseMenu.Restart();
    }

    public void UnlockAllTrophies()
    {

        LevelManager.UnlockAllWorlds();

        foreach (GameObject World in LevelManager.WorldsList)
        {
            ListofLevels CurrentWorld = World.GetComponent<ListofLevels>();
            foreach (GameObject Level in CurrentWorld.LevelsWithinWorld)
            {
                string LevelID = Level.GetComponent<LevelInfo>().SceneToLoad;
                PlayerPrefs.SetFloat(LevelID, 1);
            }
        }
        PauseMenu.Restart();
    }
    public void LockAllTrophies()
    {

        LevelManager.UnlockAllWorlds();

        foreach (GameObject World in LevelManager.WorldsList)
        {
            ListofLevels CurrentWorld = World.GetComponent<ListofLevels>();
            foreach (GameObject Level in CurrentWorld.LevelsWithinWorld)
            {
                string LevelID = Level.GetComponent<LevelInfo>().SceneToLoad;
                PlayerPrefs.SetFloat(LevelID, 260);
            }
        }
        PlayerPrefs.SetInt("GoldMedalsEarned", 0);
        PlayerPrefs.SetInt("Gold Medalist", 0);
        PauseMenu.Restart();
    }

    private void Update()
    {
        if (BuggerActive == false)
        {
            if (Input.GetKeyDown(KeyCode.B) && Input.GetKeyDown(KeyCode.U) && Input.GetKeyDown(KeyCode.G))
            {
                DebugCanvas.SetActive(true);
                BuggerActive = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.B) && Input.GetKeyDown(KeyCode.U) && Input.GetKeyDown(KeyCode.G))
            {
                DebugCanvas.SetActive(false);
                BuggerActive = false;
            }
        }

    }
}

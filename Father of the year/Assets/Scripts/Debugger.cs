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
                if (PlayerData.PlayerTimeRecords.ContainsKey(LevelID))
                {
                    PlayerData.PlayerTimeRecords.Remove(LevelID);
                }
                PlayerData.PlayerTimeRecords.Add(LevelID, 260); // sets completion time to a slow 4min 20 seconds for all levels
            }
        }
        PauseMenu.Restart();
    }

    public void UnlockAllTrophies()
    {
        UnlockAllLevels();
        foreach (GameObject World in LevelManager.WorldsList)
        {
            ListofLevels CurrentWorld = World.GetComponent<ListofLevels>();
            foreach (GameObject Level in CurrentWorld.LevelsWithinWorld)
            {
                string LevelID = Level.GetComponent<LevelInfo>().SceneToLoad;
                if (PlayerData.PlayerTimeRecords.ContainsKey(LevelID))
                {
                    PlayerData.PlayerTimeRecords.Remove(LevelID);
                }
                PlayerData.PlayerTimeRecords.Add(LevelID, 1); // sets completion time to a speedy 1 second for all golds
            }
        }
        PauseMenu.Restart();
    }
    public void LockAllTrophies()
    {
        LevelManager.LockAllWorlds();
        PlayerData.PlayerTimeRecords.Clear();
        PlayerData.AchievementRecords.Clear();
        PlayerData.TotalGoldMedals = 0;
        PlayerData.AchievementRecords.Remove("Gold Medalist");
        PlayerData.AchievementRecords.Clear();
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

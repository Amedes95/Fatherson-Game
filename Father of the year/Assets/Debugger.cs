using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public LevelManager LevelManager;
    public PauseMenu PauseMenu;


    public void UnlockAllLevels()
    {

        LevelManager.UnlockAllWorlds();

        foreach (GameObject World in LevelManager.WorldsList)
        {
            ListofLevels CurrentWorld = World.GetComponent<ListofLevels>();
            foreach (GameObject Level in CurrentWorld.LevelsWithinWorld)
            {
                string LevelID = Level.GetComponent<LevelInfo>().SceneToLoad;
                PlayerPrefs.SetFloat(LevelID, 420);
            }
        }
        PauseMenu.Restart();
    }
}

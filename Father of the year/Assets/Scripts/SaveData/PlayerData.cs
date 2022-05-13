using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class PlayerData
{
    public static PlayerData PD
    {
        get
        {
            if (playerdata == null)
            {
                playerdata = new PlayerData();
            }
            return playerdata;
        }
    }
    private static PlayerData playerdata;
    public int Tutorial_Complete;
    public int World1_Complete;
    public int World2_Complete;
    public int World3_Complete;
    public int World4_Complete;
    public int World5_Complete;
    public int World6_Complete;
    public int GoldWorld_Complete;
    public string ExitedLevel;
    public int CurrentWorld;
    public Dictionary<string, float> PlayerTimeRecords = new Dictionary<string, float>(); // keeps track of player best times
    public Dictionary<string, int> AchievementRecords = new Dictionary<string, int>(); // keep track of unlocked achievements
    public int PartyUnlocked;
    public int OldTimeyUnlocked;
    public int LifetimeDeaths;
    public int FruitCollected;
    public int TotalGoldMedals;
    public int EnemiesKilled;
    public int ApplesEaten;
    public int LollipopsEaten;
    public int CostumeIndex;
    public int GameCompleted;
    public List<LevelInfo> LevelDataList;

    public void ClearData()
    {
        Tutorial_Complete = 0;
        World1_Complete = 0;
        World2_Complete = 0;
        World3_Complete = 0;
        World4_Complete = 0;
        World5_Complete = 0;
        World6_Complete = 0;
        GoldWorld_Complete = 0;
        ExitedLevel = "Tutorial_01";
        CurrentWorld = 0;
        PartyUnlocked = 0;
        OldTimeyUnlocked = 0;
        AchievementRecords.Clear();
        PlayerTimeRecords.Clear();
        LifetimeDeaths = 0;
        FruitCollected = 0;
        TotalGoldMedals = 0;
        EnemiesKilled = 0;
        ApplesEaten = 0;
        LollipopsEaten = 0;
        CostumeIndex = 0;
        GameCompleted = 0;

        SavePlayer();
    }

    public float GetLevelBestTime(string CurrentLevelName) // searches the dictionary of best times for the key, then outputs the value of that key
    {
        float Result;
        //example: Tutorial_01, 10f
        PlayerTimeRecords.TryGetValue(CurrentLevelName, out Result);
        return Result; // return best time for level in question
    }

    public int CheckAchievementCompletion(string currentAchievement) // searches the achievement dictionary for the status of your achievement
    {
        int Result;
        AchievementRecords.TryGetValue(currentAchievement, out Result);
        return Result; // returns either 1 or 0 for unlock status
    }


    // This function only gets called when storing stored data from the computer files
    // Called when a backup is made
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log("saving game...");
    }


    // This function only gets called when loading data from the computer files
    public void LoadPlayer()
    {
        playerdata = SaveSystem.LoadPlayer();
        Debug.Log("loading data...");
        PD.SavePlayer(); // save everything one final time!
    }

    //[MenuItem("Test/TestSave")]
    //public static void TestSave()
    //{
    //    PD.SavePlayer();
    //}
    //[MenuItem("Test/TestLoad")]
    //public static void TestLoad()
    //{
    //    PD.LoadPlayer();
    //}

    //[MenuItem("Test/ClearSavedData")]
    //public static void ClearSavedData()
    //{
    //    PD.ClearData();
    //    Debug.Log("Clearing save data...");
    //}

    //[MenuItem("Test/DirtyBackupPlayerPrefs")]
    //public static void DirtyBackupPlayerPrefs()
    //{
    //    PlayerPrefs.DeleteAll();
    //    Debug.Log("Backup prefs dirtied");
    //    PlayerPrefs.SetInt("SaveFileVersioning", 0);
    //    PlayerPrefs.SetInt("LevelTimeBackup", 0);
    //    PlayerPrefs.SetInt("AchievementBackups", 0);
    //    // test player scenario
    //    PlayerPrefs.SetFloat("Tutorial_01", 100);
    //    PlayerPrefs.SetFloat("Tutorial_02", 200);
    //    PlayerPrefs.SetFloat("Tutorial_03", 300);
    //    PlayerPrefs.SetString("ExitedLevel", "Tutorial_04");
    //    PlayerPrefs.SetInt("CurrentWorld", 0);
    //    PlayerPrefs.SetInt("Bonk!", 1);
    //    PlayerPrefs.SetInt("Jackpot!", 1);
    //    PlayerPrefs.SetInt("Indigestible", 1);
    //    PlayerPrefs.SetInt("Couch Potato", 1);
    //    PlayerPrefs.SetInt("Vegetarian", 1);
    //    PlayerPrefs.SetInt("Nutritious!", 1);
    //    PlayerPrefs.SetInt("ApplesEaten", 5);
    //    PlayerPrefs.SetInt("GameCompleted", 1);
    //}

}


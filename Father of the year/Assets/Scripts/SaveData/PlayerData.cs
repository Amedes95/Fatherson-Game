using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    public static int Tutorial_Complete;
    public static int World1_Complete;
    public static int World2_Complete;
    public static int World3_Complete;
    public static int World4_Complete;
    public static int World5_Complete;
    public static int World6_Complete;
    public static int GoldWorld_Complete;
    public static string ExitedLevel;
    public static int CurrentWorld;
    public static Dictionary<string, float> PlayerTimeRecords = new Dictionary<string, float>(); // keeps track of player best times
    public static Dictionary<string, int> AchievementRecords = new Dictionary<string, int>(); // keep track of unlocked achievements
    public static int PartyUnlocked;
    public static int OldTimeyUnlocked;

    public static int LifetimeDeaths;
    public static int FruitCollected;
    public static int TotalGoldMedals;
    public static int EnemiesKilled;
    public static int ApplesEaten;
    public static int LollipopsEaten;
    public static int CostumeIndex;
    public static int GameCompleted;


    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Player data");
    }


    public static void ClearData()
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

    }

    public static float GetLevelBestTime(string CurrentLevelName) // searches the dictionary of best times for the key, then outputs the value of that key
    {
        float Result;
        //example: Tutorial_01, 10f
        PlayerTimeRecords.TryGetValue(CurrentLevelName, out Result);
        return Result; // return best time for level in question
    }

    public static int CheckAchievementCompletion(string currentAchievement) // searches the achievement dictionary for the status of your achievement
    {
        int Result;
        AchievementRecords.TryGetValue(currentAchievement, out Result);
        return Result; // returns either 1 or 0 for unlock status
    }

}


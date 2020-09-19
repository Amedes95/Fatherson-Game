using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Steamworks;

public class AchievementInfo : MonoBehaviour
{

    public string AchievementTitle;
    public string AchievementDescription;
    public bool Locked;
    public bool Secret;
    public GameObject LockedSymbol;

    // Start is called before the first frame update
    void Awake()
    {
        Locked = true;
        LockedSymbol.SetActive(true);
        if (PlayerPrefs.GetInt(AchievementTitle) == 1)
        {
            SteamUserStats.SetAchievement(AchievementTitle); // If you DO have the achievement lcoally but steam doesn't think so, update it here
            SteamUserStats.StoreStats();
        }
        else
        {
            SteamUserStats.ClearAchievement(AchievementTitle); // if you DONT have the achievement locally but steam says you do, remove it from steam
            SteamUserStats.StoreStats();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Locked == false)
        {
            LockedSymbol.SetActive(false);
        }
        else
        {
            LockedSymbol.SetActive(true);
        }
        if (PlayerPrefs.GetInt(AchievementTitle) == 1)
        {
            Locked = false;
        }
        else
        {
            Locked = true;
        }

    }
}

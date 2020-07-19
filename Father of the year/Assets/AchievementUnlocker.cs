using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementUnlocker : MonoBehaviour
{
    public string AchievementToUnlock;

    Boombox Boombox;

    public void Start()
    {
        GameObject.FindGameObjectWithTag("Boombox").GetComponent<Boombox>();
    }

    public void UnlockCheevo()
    {
        Boombox.UnlockCheevo(AchievementToUnlock);
    }
}

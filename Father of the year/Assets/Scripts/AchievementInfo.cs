using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

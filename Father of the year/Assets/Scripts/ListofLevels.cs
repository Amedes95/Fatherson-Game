using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListofLevels : MonoBehaviour
{
    public List<GameObject> LevelsWithinWorld;

    public int CurrentIndex;

    float SmallDelay;
    bool ready;



    private void Awake() // for some reason, there needs to be a .1 frame delay or else the information isn't read correctly
    {
        ready = true;
        SmallDelay = .1f;
    }

    private void FixedUpdate()
    {
        if (ready)
        {
            SmallDelay -= Time.smoothDeltaTime;
            if (SmallDelay <= 0)
            {
                UnlockLevels();
                ready = false;
            }
        }


    }
    

    public void UnlockLevels()
    {
        LevelsWithinWorld[0].GetComponent<LevelInfo>().Unlocked = true; // update level 0 to be always unlocked
        for (int i = 1; i < LevelsWithinWorld.Count; i++)
        {
            string SceneToLoad = LevelsWithinWorld[i - 1].GetComponent<LevelInfo>().SceneToLoad;
            if (PlayerData.PD.PlayerTimeRecords.ContainsKey(SceneToLoad)) // If you have a time saved for the previous one, unlock me next. Time will be lsited in dictionary
            {
                LevelsWithinWorld[i].GetComponent<LevelInfo>().Unlocked = true;
                //Debug.Log("New level unlocked");
            }
        }
    }
    

}

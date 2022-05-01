using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalLevel : MonoBehaviour
{

    public int WorldNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name == "Tutorial_20") // if its the last level of the tutorial
            {
                PlayerData.Tutorial_Complete = 1;
                /// Unlocks Baby Steps Achievement
                if (PlayerData.AchievementRecords.ContainsKey("Baby Steps") == false) // not already unlocked?
                {
                    PlayerData.AchievementRecords.Add("Baby Steps", 1);
                    Debug.Log("Baby Steps Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Baby Steps");
                }
            }
            if (SceneManager.GetActiveScene().name == "CarrotRescue") // rescue carrot, unlock new world
            {
                PlayerData.World1_Complete = 1;
            }
            if (SceneManager.GetActiveScene().name == "W1BOSS") // w1 boss beaten, unlock cheevo
            {
                /// Unlocks Flea Flee! Achievement
                if (PlayerData.AchievementRecords.ContainsKey("Flea Flee") == false) // not already unlocked?
                {
                    PlayerData.AchievementRecords.Add("Flea Flee", 1); // add to unlock dictionary
                    Debug.Log("Flea Flee Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Flea Flee");
                }
            }
            if (SceneManager.GetActiveScene().name == "TurnipRescue") // turnip rescued, unlock new world
            {
                PlayerData.World2_Complete = 1;

            }
            if (SceneManager.GetActiveScene().name == "W2BOSS") // w2 boss beaten, unlock cheevo
            {
                /// Unlocks Yee Haw! Achievement
                if (PlayerData.AchievementRecords.ContainsKey("Spoiled Appetite") == false) // not unlocked already?
                {
                    PlayerData.AchievementRecords.Add("Spoiled Appetite", 1); // not unlocked already?
                    Debug.Log("Spoiled Appetite");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Spoiled Appetite");
                }
            }
            if (SceneManager.GetActiveScene().name == "CornRescue") // rescue corn, unlock new world
            {
                PlayerData.World3_Complete = 1;

            }
            /// Unlocks Party Crasher Achievement
            if (SceneManager.GetActiveScene().name == "PartyEnd") // unlock in party world
            {
                if (PlayerData.AchievementRecords.ContainsKey("Party Crasher") == false) // not unlocked already?
                {
                    PlayerData.AchievementRecords.Add("Party Crasher", 1); // add to unlock dictionary
                    Debug.Log("Party Crasher Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Party Crasher");
                    PlayerData.PartyUnlocked = 1;
                }

            }
            /// Unlocks Fossilized Achievement
            if (SceneManager.GetActiveScene().name == "RetroEnd") // unlock by beating retro world
            {
                if (PlayerData.AchievementRecords.ContainsKey("Fossilized") == false) // not unlocked already?
                {
                    PlayerData.AchievementRecords.Add("Fossilized", 1); //add to unlock dictionary
                    Debug.Log("Fossilized Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Fossilized");
                    PlayerData.OldTimeyUnlocked = 1;
                }

            }
            if (SceneManager.GetActiveScene().name == "W3BOSS") // w3 boss beaten, unlock cheevo
            {
                /// Unlocks Fungus Among Us Achievement
                if (PlayerData.AchievementRecords.ContainsKey("Fungus Among Us") == false) // not unlocked already?
                {
                    PlayerData.AchievementRecords.Add("Fungus Among Us", 1); // add to unlock dictionary
                    Debug.Log("Fungus Among Us");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Fungus Among Us");
                }
            }
            if (SceneManager.GetActiveScene().name == "TomatoRescue") // rescue tomato, unlock new world
            {
                PlayerData.World4_Complete = 1;

            }
            if (SceneManager.GetActiveScene().name == "W4BOSS") // w4 boss beaten, unlock cheevo
            {
                /// Unlocks Ghastly Escape Achievement
                if (PlayerData.AchievementRecords.ContainsKey("Ghastly Escape") == false) // not unlocked already?
                {
                    PlayerData.AchievementRecords.Add("Ghastly Escape", 1); // add to unlock dictionary
                    Debug.Log("Ghastly Escape");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Ghastly Escape");
                }
            }
            if (SceneManager.GetActiveScene().name == "PotatoRescue") // rescue potato, unlock new world
            {
                PlayerData.World5_Complete = 1;

            }
            if (SceneManager.GetActiveScene().name == "W5BOSS") // w5 boss beaten, unlock cheevo
            {
                /// Unlocks Frostbitten Achievement
                if (PlayerData.AchievementRecords.ContainsKey("Frostbitten") == false) // not unlocked already?
                {
                    PlayerData.AchievementRecords.Add("Frostbitten", 1);
                    Debug.Log("Frostbitten");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Frostbitten");
                }
            }
            if (SceneManager.GetActiveScene().name == "W6BOSS") // w6 boss beaten
            {
                PlayerData.World6_Complete = 1;
                /// Unlocks Hippidy Hoppidy Achievement
                if (PlayerData.AchievementRecords.ContainsKey("Hippidy Hoppidy") == false) // not unlocked already?
                {
                    PlayerData.AchievementRecords.Add("Hippidy Hoppidy", 1); // add to unlock dictionary
                    Debug.Log("Hippidy Hoppidy");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Hippidy Hoppidy");
                }
            }
            if (SceneManager.GetActiveScene().name == "GoldEnd") // unlocked by beating gold world
            {
                PlayerData.GoldWorld_Complete = 1;
                // unlocks Ancient Evil achievement
                if (PlayerData.AchievementRecords.ContainsKey("Ancient Evil") == false) // not unlocked already?
                {
                    PlayerData.AchievementRecords.Add("Ancient Evil", 1); // add to unlock dictionary
                    Debug.Log("Ancient Evil");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Ancient Evil");
                }
            }


            // unlocks Vegetarian achievement
            if (PlayerData.AchievementRecords.ContainsKey("Vegetarian") == false && PlayerPrefs.GetInt("Flawless Run") == 0) // not unlocked already?
            {
                PlayerData.AchievementRecords.Add("Vegetarian", 1); // add to unlock dictionary
                Debug.Log("Vegetarian");
                BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                BGMusic.UnlockCheevo("Vegetarian");
            }




            //// unlocks achievements for beating non boss levels

            if (WorldNumber == 1) // last non-boss level in world 1
            {
                /// Unlocks Snake Charmer Achievement
                if (PlayerData.AchievementRecords.ContainsKey("Snake charmer") == false) // not unlocked already?
                {
                    PlayerData.AchievementRecords.Add("Snake charmer", 1); // add to unlock dictionary
                    Debug.Log("Snake charmer Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Snake charmer");
                }
            }
            else if (WorldNumber == 2)
            {
                /// Unlocks World Wide Web Achievement
                if (PlayerData.AchievementRecords.ContainsKey("World Wide Web") == false) // not unlocked already?
                {
                    PlayerData.AchievementRecords.Add("World Wide Web", 1); // add to unlock dictionary
                    Debug.Log("World Wide Web Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("World Wide Web");
                }
            }
            else if (WorldNumber == 3)
            {
                /// Unlocks Synthetic Scientist Achievement
                if (PlayerData.AchievementRecords.ContainsKey("Synthetic Scientist") == false) // not unlocked already?
                {
                    PlayerData.AchievementRecords.Add("Synthetic Scientist", 1); // add to unlock dictionary
                    Debug.Log("Synthetic Scientist Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Synthetic Scientist");
                }
            }
            else if (WorldNumber == 4)
            {
                /// Unlocks Hot Topic Achievement
                if (PlayerData.AchievementRecords.ContainsKey("Hot Topic") == false) // not unlocked already?
                {
                    PlayerData.AchievementRecords.Add("Hot Topic", 1); // add to unlock dictionary
                    Debug.Log("Hot Topic Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Hot Topic");
                }
            }
            else if (WorldNumber == 5)
            {
                /// Unlocks Cold Shoulder Achievement
                if (PlayerData.AchievementRecords.ContainsKey("Cold Shoulder") == false) // not unlocked already?
                {
                    PlayerData.AchievementRecords.Add("Cold Shoulder", 1); // add to unlock dictionary
                    Debug.Log("Cold Shoulder");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Cold Shoulder");
                }
            }
            else if (WorldNumber == 6)
            {
                /// Unlocks The End? Achievement
                if (PlayerData.AchievementRecords.ContainsKey("The End?") == false) // not unlocked already?
                {
                    PlayerData.AchievementRecords.Add("The End?", 1); // add to unlock dictionary
                    Debug.Log("The End?");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("The End?");
                }
            }
        }
    }
}

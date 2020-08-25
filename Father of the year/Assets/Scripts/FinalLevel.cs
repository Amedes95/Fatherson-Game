﻿using System.Collections;
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
                PlayerPrefs.SetInt("Tutorial_Complete", 1);
                /// Unlocks Baby Steps Achievement
                if (PlayerPrefs.GetInt("Baby steps") == 0)
                {
                    PlayerPrefs.SetInt("Baby steps", 1);
                    Debug.Log("Baby steps Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Baby steps");
                }
            }
            if (SceneManager.GetActiveScene().name == "CarrotRescue") // rescue carrot, unlock new world
            {
                PlayerPrefs.SetInt("World1_Complete", 1);
            }
            if (SceneManager.GetActiveScene().name == "W1BOSS") // w1 boss beaten, unlock cheevo
            {
                /// Unlocks Flea Flee! Achievement
                if (PlayerPrefs.GetInt("Flea Flee") == 0)
                {
                    PlayerPrefs.SetInt("Flea Flee", 1);
                    Debug.Log("Flea Flee Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Flea Flee");
                }
                /// Unlocks Disco Fever Achievement
                if (PlayerPrefs.GetInt("Disco Fever") == 0 && PlayerPrefs.GetInt("PartyModeON") == 1)
                {
                    PlayerPrefs.SetInt("Disco Fever", 1);
                    Debug.Log("Disco Fever Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Disco Fever");
                }
            }
            if (SceneManager.GetActiveScene().name == "TurnipRescue") // turnip rescued, unlock new world
            {
                PlayerPrefs.SetInt("World2_Complete", 1);
            }
            if (SceneManager.GetActiveScene().name == "W2BOSS") // w2 boss beaten, unlock cheevo
            {
                /// Unlocks Yee Haw! Achievement
                if (PlayerPrefs.GetInt("Spoiled Appetite") == 0)
                {
                    PlayerPrefs.SetInt("Spoiled Appetite", 1);
                    Debug.Log("Spoiled Appetite");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Spoiled Appetite");
                }
                /// Unlocks Disco Fever Achievement
                if (PlayerPrefs.GetInt("Disco Fever") == 0 && PlayerPrefs.GetInt("PartyModeON") == 1)
                {
                    PlayerPrefs.SetInt("Disco Fever", 1);
                    Debug.Log("Disco Fever Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Disco Fever");
                }
            }
            if (SceneManager.GetActiveScene().name == "CornRescue") // rescue corn, unlock new world
            {
                PlayerPrefs.SetInt("World3_Complete", 1);
            }
            /// Unlocks Party Crasher Achievement
            if (SceneManager.GetActiveScene().name == "PartyEnd") // unlock in party world
            {
                if (PlayerPrefs.GetInt("Party Crasher") == 0)
                {
                    PlayerPrefs.SetInt("Party Crasher", 1);
                    Debug.Log("Party Crasher Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Party Crasher");
                    PlayerPrefs.SetInt("PartyUnlocked", 1);
                }

            }
            /// Unlocks Fossilized Achievement
            if (SceneManager.GetActiveScene().name == "RetroEnd") // unlock by beating retro world
            {
                if (PlayerPrefs.GetInt("Fossilized") == 0)
                {
                    PlayerPrefs.SetInt("Fossilized", 1);
                    Debug.Log("Fossilized Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Fossilized");
                    PlayerPrefs.SetInt("OldTimeyUnlocked", 1);
                }

            }
            if (SceneManager.GetActiveScene().name == "W3BOSS") // w3 boss beaten, unlock cheevo
            {
                /// Unlocks Fungus Among Us Achievement
                if (PlayerPrefs.GetInt("Fungus Among Us") == 0)
                {
                    PlayerPrefs.SetInt("Fungus Among Us", 1);
                    Debug.Log("Fungus Among Us");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Fungus Among Us");
                }
                /// Unlocks Disco Fever Achievement
                if (PlayerPrefs.GetInt("Disco Fever") == 0 && PlayerPrefs.GetInt("PartyModeON") == 1)
                {
                    PlayerPrefs.SetInt("Disco Fever", 1);
                    Debug.Log("Disco Fever Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Disco Fever");
                }
            }
            if (SceneManager.GetActiveScene().name == "TomatoRescue") // rescue tomato, unlock new world
            {
                PlayerPrefs.SetInt("World4_Complete", 1);
            }
            if (SceneManager.GetActiveScene().name == "W4BOSS") // w4 boss beaten, unlock cheevo
            {
                /// Unlocks Ghastly Escape Achievement
                if (PlayerPrefs.GetInt("Ghastly Escape") == 0)
                {
                    PlayerPrefs.SetInt("Ghastly Escape", 1);
                    Debug.Log("Ghastly Escape");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Ghastly Escape");
                }
                /// Unlocks Disco Fever Achievement
                if (PlayerPrefs.GetInt("Disco Fever") == 0 && PlayerPrefs.GetInt("PartyModeON") == 1)
                {
                    PlayerPrefs.SetInt("Disco Fever", 1);
                    Debug.Log("Disco Fever Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Disco Fever");
                }
            }
            if (SceneManager.GetActiveScene().name == "PotatoRescue") // rescue potato, unlock new world
            {
                PlayerPrefs.SetInt("World5_Complete", 1);
            }
            if (SceneManager.GetActiveScene().name == "W5BOSS") // w5 boss beaten, unlock cheevo
            {
                /// Unlocks Frostbitten Achievement
                if (PlayerPrefs.GetInt("Frostbitten") == 0)
                {
                    PlayerPrefs.SetInt("Frostbitten", 1);
                    Debug.Log("Frostbitten");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Frostbitten");
                }
                /// Unlocks Disco Fever Achievement
                if (PlayerPrefs.GetInt("Disco Fever") == 0 && PlayerPrefs.GetInt("PartyModeON") == 1)
                {
                    PlayerPrefs.SetInt("Disco Fever", 1);
                    Debug.Log("Disco Fever Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Disco Fever");
                }
            }
            if (SceneManager.GetActiveScene().name == "W6BOSS") // w6 boss beaten
            {
                PlayerPrefs.SetInt("World6_Complete", 1);
                /// Unlocks Hippidy Hoppidy Achievement
                if (PlayerPrefs.GetInt("Hippidy Hoppidy") == 0)
                {
                    PlayerPrefs.SetInt("Hippidy Hoppidy", 1);
                    Debug.Log("Hippidy Hoppidy");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Hippidy Hoppidy");
                }
            }
            if (SceneManager.GetActiveScene().name == "GoldEnd") // unlocked by beating gold world
            {
                PlayerPrefs.SetInt("GoldWorld_Complete", 1);
                // unlocks Ancient Evil achievement
                if (PlayerPrefs.GetInt("Ancient Evil") == 0)
                {
                    PlayerPrefs.SetInt("Ancient Evil", 1);
                    Debug.Log("Ancient Evil");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Ancient Evil");
                }
            }


            // unlocks Vegetarian achievement
            if (PlayerPrefs.GetInt("Vegetarian") == 0 && PlayerPrefs.GetInt("Flawless Run") == 0)
            {
                PlayerPrefs.SetInt("Vegetarian", 1);
                Debug.Log("Vegetarian");
                BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                BGMusic.UnlockCheevo("Vegetarian");
            }




            //// unlocks achievements for beating non boss levels

            if (WorldNumber == 1) // last non-boss level in world 1
            {
                /// Unlocks Snake Charmer Achievement
                if (PlayerPrefs.GetInt("Snake charmer") == 0)
                {
                    PlayerPrefs.SetInt("Snake charmer", 1);
                    Debug.Log("Snake charmer Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Snake charmer");
                }
            }
            else if (WorldNumber == 2)
            {
                /// Unlocks World Wide Web Achievement
                if (PlayerPrefs.GetInt("World Wide Web") == 0)
                {
                    PlayerPrefs.SetInt("World Wide Web", 1);
                    Debug.Log("World Wide Web Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("World Wide Web");
                }
            }
            else if (WorldNumber == 3)
            {
                /// Unlocks Synthetic Scientist Achievement
                if (PlayerPrefs.GetInt("Synthetic Scientist") == 0)
                {
                    PlayerPrefs.SetInt("Synthetic Scientist", 1);
                    Debug.Log("Synthetic Scientist Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Synthetic Scientist");
                }
            }
            else if (WorldNumber == 4)
            {
                /// Unlocks Hot Topic Achievement
                if (PlayerPrefs.GetInt("Hot Topic") == 0)
                {
                    PlayerPrefs.SetInt("Hot Topic", 1);
                    Debug.Log("Hot Topic Unlocked");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Hot Topic");
                }
            }
            else if (WorldNumber == 5)
            {
                /// Unlocks Cold Shoulder Achievement
                if (PlayerPrefs.GetInt("Cold Shoulder") == 0)
                {
                    PlayerPrefs.SetInt("Cold Shoulder", 1);
                    Debug.Log("Cold Shoulder");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("Cold Shoulder");
                }
            }
            else if (WorldNumber == 6)
            {
                /// Unlocks The End? Achievement
                if (PlayerPrefs.GetInt("The End?") == 0)
                {
                    PlayerPrefs.SetInt("The End?", 1);
                    Debug.Log("The End?");
                    BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                    BGMusic.UnlockCheevo("The End?");
                }
            }
        }
    }
}

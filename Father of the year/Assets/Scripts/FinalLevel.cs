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
                PlayerPrefs.SetInt("Tutorial_Complete", 1);
                /// Unlocks BAby Steps Achievement
                if (PlayerPrefs.GetInt("Baby steps") == 0)
                {
                    PlayerPrefs.SetInt("Baby steps", 1);
                    Debug.Log("Baby steps Unlocked");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("Baby steps");
                }
            }
            if (SceneManager.GetActiveScene().name == "W1BOSS") // w1 boss beaten
            {
                PlayerPrefs.SetInt("World1_Complete", 1);
                /// Unlocks Flea Flee! Achievement
                if (PlayerPrefs.GetInt("Flea Flee!") == 0)
                {
                    PlayerPrefs.SetInt("Flea Flee!", 1);
                    Debug.Log("Flea Flee! Unlocked");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("Flea Flee!");
                }

            }
            if (SceneManager.GetActiveScene().name == "W2BOSS") // w2 boss beaten
            {
                PlayerPrefs.SetInt("World2_Complete", 1);
                /// Unlocks Yee Haw! Achievement
                if (PlayerPrefs.GetInt("Yee Haw!") == 0)
                {
                    PlayerPrefs.SetInt("Yee Haw!", 1);
                    Debug.Log("Yee Haw!");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("Yee Haw!");
                }
            }
            if (SceneManager.GetActiveScene().name == "W3BOSS") // w3 boss beaten
            {
                PlayerPrefs.SetInt("World3_Complete", 1);
                /// Unlocks Fungus Among Us Achievement
                if (PlayerPrefs.GetInt("Fungus Among Us") == 0)
                {
                    PlayerPrefs.SetInt("Fungus Among Us", 1);
                    Debug.Log("Fungus Among Us");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("Fungus Among Us");
                }
            }
            if (SceneManager.GetActiveScene().name == "W4BOSS") // w4 boss beaten
            {
                PlayerPrefs.SetInt("World4_Complete", 1);
                /// Unlocks Ghastly Escape Achievement
                if (PlayerPrefs.GetInt("Ghastly Escape") == 0)
                {
                    PlayerPrefs.SetInt("Ghastly Escape", 1);
                    Debug.Log("Ghastly Escape");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("Ghastly Escape");
                }
            }
            if (SceneManager.GetActiveScene().name == "W5BOSS") // w5 boss beaten
            {
                PlayerPrefs.SetInt("World5_Complete", 1);
                /// Unlocks Frostbitten Achievement
                if (PlayerPrefs.GetInt("Frostbitten") == 0)
                {
                    PlayerPrefs.SetInt("Frostbitten", 1);
                    Debug.Log("Frostbitten");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("Frostbitten");
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
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("Hippidy Hoppidy");
                }
            }




            //// unlocks achievements for beating non boss levels

            if (WorldNumber == 1) // last non-boss level in world 1
            {
                /// Unlocks Snake Charmer Achievement
                if (PlayerPrefs.GetInt("Snake charmer") == 0)
                {
                    PlayerPrefs.SetInt("Snake charmer", 1);
                    Debug.Log("Snake charmer Unlocked");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("Snake charmer");
                }
            }
            else if (WorldNumber == 2)
            {
                /// Unlocks World Wide Web Achievement
                if (PlayerPrefs.GetInt("World Wide Web") == 0)
                {
                    PlayerPrefs.SetInt("World Wide Web", 1);
                    Debug.Log("World Wide Web Unlocked");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("World Wide Web");
                }
            }
            else if (WorldNumber == 3)
            {
                /// Unlocks Synthetic Scientist Achievement
                if (PlayerPrefs.GetInt("Synthetic Scientist") == 0)
                {
                    PlayerPrefs.SetInt("Synthetic Scientist", 1);
                    Debug.Log("Synthetic Scientist Unlocked");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("Synthetic Scientist");
                }
            }
            else if (WorldNumber == 4)
            {
                /// Unlocks Hot Topic Achievement
                if (PlayerPrefs.GetInt("Hot Topic") == 0)
                {
                    PlayerPrefs.SetInt("Hot Topic", 1);
                    Debug.Log("Hot Topic Unlocked");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("Hot Topic");
                }
            }
            else if (WorldNumber == 5)
            {
                /// Unlocks Cold Shoulder Achievement
                if (PlayerPrefs.GetInt("Cold Shoulder") == 0)
                {
                    PlayerPrefs.SetInt("Cold Shoulder", 1);
                    Debug.Log("Cold Shoulder");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("Cold Shoulder");
                }
            }
            else if (WorldNumber == 6)
            {
                /// Unlocks The End? Achievement
                if (PlayerPrefs.GetInt("The End?") == 0)
                {
                    PlayerPrefs.SetInt("The End?", 1);
                    Debug.Log("The End?");
                    Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                    Boombox.UnlockCheevo("The End?");
                }
            }
        }
    }
}

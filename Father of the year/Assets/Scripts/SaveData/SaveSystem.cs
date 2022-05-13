using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Saves player data to a persistent file storage on the computer.  Used to push up cloud data to Steam.
/// </summary>
public static class SaveSystem
{
    public static void SavePlayer (PlayerData PlayerSave)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dat";
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, PlayerSave);
        }

        
    }

    public static PlayerData LoadPlayer()
    {
        PlayerData data;

        string path = Application.persistentDataPath + "/player.dat";
        if (File.Exists(path)) // should already exist on a new machine if cloud backup worked // TODO: re-unlock achievements
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                data = formatter.Deserialize(stream) as PlayerData;
                Debug.Log("Filepath already exists, returning saved data from binary");
                return data;
            }
        }
        else // should trigger always for old players upgrading to this new system
        {
            data = new PlayerData();
            Debug.Log("No save file file, creating a new one");
        }

        // This gets triggered if a player has existing data from the original release of the game.
        // We cannot accidentally wipe their data. Meant to be a 1 time conversion.
        if (PlayerPrefs.GetInt("SaveFileVersioning", 0) != 1) // old players will never have this new playerpref, so default it to 0
        {
            Debug.Log("legacy save system detected, extracting old data");
            /// extract important data from playerpref fields
            data.Tutorial_Complete = PlayerPrefs.GetInt("Tutorial_Complete");
            data.World1_Complete = PlayerPrefs.GetInt("World1_Complete");
            data.World2_Complete = PlayerPrefs.GetInt("World2_Complete");
            data.World3_Complete = PlayerPrefs.GetInt("World3_Complete");
            data.World4_Complete = PlayerPrefs.GetInt("World4_Complete");
            data.World5_Complete = PlayerPrefs.GetInt("World5_Complete");
            data.World6_Complete = PlayerPrefs.GetInt("World6_Complete");
            data.GoldWorld_Complete = PlayerPrefs.GetInt("GoldWorld_Complete");
            data.ExitedLevel = PlayerPrefs.GetString("ExitedLevel");
            data.CurrentWorld = PlayerPrefs.GetInt("CurrentWorld");
            data.PartyUnlocked = PlayerPrefs.GetInt("PartyUnlocked");
            data.OldTimeyUnlocked = PlayerPrefs.GetInt("OldTimeyUnlocked");
            data.LifetimeDeaths = PlayerPrefs.GetInt("Deathcount"); // name was changed
            data.FruitCollected = PlayerPrefs.GetInt("ApplesEaten"); // name was changed
            data.TotalGoldMedals = PlayerPrefs.GetInt("GoldMedalsEarned"); // name was changed
            data.EnemiesKilled = PlayerPrefs.GetInt("EnemiesKilled");
            data.ApplesEaten = PlayerPrefs.GetInt("ApplesEaten");
            data.LollipopsEaten = PlayerPrefs.GetInt("LollipopsEaten");
            data.CostumeIndex = PlayerPrefs.GetInt("CostumeIndex");
            data.GameCompleted = PlayerPrefs.GetInt("GameCompleted");
            PlayerPrefs.SetInt("SaveFileVersioning", 1); // never speak of this EVER AGAIN
        }

        return data;
    }
}

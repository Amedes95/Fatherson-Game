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
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                data = formatter.Deserialize(stream) as PlayerData;
                return data;
            }
        }
        else
        {
            data = new PlayerData();
        }

        if (PlayerPrefs.GetInt("SaveFileVersioning", 0) != 1)
        {
            /// person only has player pref data
            /// transfer to new system
            /// mark save file versioning as 1 -- player best time and level completions (W1_01, 420f)
            //data.... // get the things here

            PlayerPrefs.SetInt("SaveFileVersioning", 1);
        }

        return data;
    }
}

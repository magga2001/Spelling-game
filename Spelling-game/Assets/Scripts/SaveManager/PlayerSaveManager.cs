using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerSaveManager
{
    public static void SavePlayerInfo(int highScore, List<string> correctWords, List<string> IncorrectWords)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.exe";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(highScore, correctWords,IncorrectWords);

        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayerInfo()
    {
        string path = Application.persistentDataPath + "/player.exe";

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = binaryFormatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in" + path);
            return null;
        }

    }

    public static void DeleteProgess()
    {
        Debug.Log("DELETING");
        string path = Application.persistentDataPath + "/player.exe";

        File.Delete(path);
    }
}

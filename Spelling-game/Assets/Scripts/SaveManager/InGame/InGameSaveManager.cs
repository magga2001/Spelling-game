using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//This code is inspired from
//Thirslund, A. (Director). (2018, December 02). SAVE &amp; LOAD SYSTEM in Unity [Video file]. Retrieved February 15, 2023, from https://www.youtube.com/watch?v=XOjd_qU2Ido
public class InGameSaveManager
{
    public static void SaveInfo(int score, int health, int lives)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/ingame.exe";
        FileStream stream = new FileStream(path, FileMode.Create);

        InGameData data = new InGameData(score, health, lives);

        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }

    public static InGameData LoadInfo()
    {
        string path = Application.persistentDataPath + "/ingame.exe";

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InGameData data = binaryFormatter.Deserialize(stream) as InGameData;

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
        string path = Application.persistentDataPath + "/ingame.exe";

        File.Delete(path);
    }
}

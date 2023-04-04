using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//This code is inspired from
//Thirslund, A. (Director). (2018, December 02). SAVE &amp; LOAD SYSTEM in Unity [Video file]. Retrieved February 15, 2023, from https://www.youtube.com/watch?v=XOjd_qU2Ido
public class PlayerSaveManager
{
    public static void SaveInfo(List<HighScoreData> highScores, List<string> correctWords, List<string> IncorrectWords)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.exe";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new(highScores, correctWords, IncorrectWords);

        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadInfo()
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

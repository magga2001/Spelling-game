using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

//This code is inspired from
//Thirslund, A. (Director). (2018, December 02). SAVE &amp; LOAD SYSTEM in Unity [Video file]. Retrieved February 15, 2023, from https://www.youtube.com/watch?v=XOjd_qU2Ido
public class PuzzleSaveManager
{
    public static void SaveInfo(string fileName, List<Cell> board, int rows, int columns)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        string path = Application.persistentDataPath + fileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        PuzzlesData data = new PuzzlesData(board, rows, columns);

        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }

    public static PuzzlesData LoadInfoFromFile(string fileName)
    {
        string path = Application.persistentDataPath + fileName;
                       
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PuzzlesData data = binaryFormatter.Deserialize(stream) as PuzzlesData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in" + path);
            return null;
        }

    }

    public static void DeleteProgess(string fileName)
    {
        string path = Application.persistentDataPath + fileName;

        File.Delete(path);
    }
}
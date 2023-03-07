using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class PuzzleSaveManager
{
    public static void SaveInfo(string fileName, string[,] board, int rows, int columns)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        string path = Application.persistentDataPath + fileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        PuzzleData data = new PuzzleData(board, rows, columns);

        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }

    public static PuzzleData LoadInfoFromFile(string fileName)
    {
        string path = Application.persistentDataPath + fileName;
                       
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PuzzleData data = binaryFormatter.Deserialize(stream) as PuzzleData;

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
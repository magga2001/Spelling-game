using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu]
public class BoardData : ScriptableObject
{
    [HideInInspector] [SerializeField] private string Id;
    private int rows;
    private int columns;

    public string ID { get { return Id; } set { Id = value; } }
    public int Rows { get { return rows; } set { rows = value; } }
    public int Columns { get { return columns; } set { columns = value; } }

    private string[,] board;

    [SerializeField] private List<string> words;
    public List<string> Words { get { return words; } set { words = value; } }

    public void CreateNewBoard()
    {
        board = new string[columns, rows];

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                board[i,j] = "";
            }
        }
    }

    public void LoadSavedBoard()
    {
        try
        {
            board = PuzzleSaveManager.LoadInfoFromFile(FileName.puzzle + ID).board;
            rows = PuzzleSaveManager.LoadInfoFromFile(FileName.puzzle + ID).rows;  
            columns = PuzzleSaveManager.LoadInfoFromFile(FileName.puzzle + ID).columns;
        }
        catch
        {
            Debug.Log("File not found");
        }
    }

    public void UpdateBoardData(int column, int row, string data)
    {
        board[column, row] = data;
    }

    public string[,] GetBoardData()
    {
        return board;
    }

    public void ClearBoard()
    {
        Array.Clear(board, 0, board.Length);
        CreateNewBoard();
    }

    public void SaveBoard(string id)
    {
        ID = id;
        PuzzleSaveManager.SaveInfo(FileName.puzzle + ID, board, rows, columns);
    }

    public void DeleteSavedBoard()
    {
        PuzzleSaveManager.DeleteProgess(FileName.puzzle + ID);
    }
    public (string[,] board, int rows , int columns) GetSavedBoardData()
    {
        LoadSavedBoard();
        return (board, rows, columns);
    }
}

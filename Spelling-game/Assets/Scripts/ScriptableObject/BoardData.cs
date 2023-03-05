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
    private int rows;
    private int columns;

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
            board = PuzzleSaveManager.LoadInfoFromFile(FileName.puzzle).board;
            rows = PuzzleSaveManager.LoadInfoFromFile(FileName.puzzle).rows;  
            columns = PuzzleSaveManager.LoadInfoFromFile(FileName.puzzle).columns;
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

    public void SaveBoard()
    {
        PuzzleSaveManager.SaveInfo(FileName.puzzle, board, rows, columns);
    }

    public void DeleteSavedBoard()
    {
        PuzzleSaveManager.DeleteProgess(FileName.puzzle);
    }

    public (string[,] board, int rows , int columns) GetSavedBoardData()
    {
        LoadSavedBoard();
        return (board, rows, columns);
    }
}

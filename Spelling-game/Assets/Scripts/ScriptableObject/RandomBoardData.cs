using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class RandomBoardData : ScriptableObject
{
    private int rows;
    private int columns;

    public int Rows { get { return rows; } set { rows = value; } }
    public int Columns { get { return columns; } set { columns = value; } }

    private string[,] board;

    public void CreateNewBoard()
    {
        board = new string[columns, rows];

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                board[i, j] = "";
            }
        }
    }

    public void LoadSavedBoard()
    {
        try
        {
            board = SaveManager.LoadInfoFromFile(FileName.randomPuzzle).board;
            rows = SaveManager.LoadInfoFromFile(FileName.randomPuzzle).rows;
            columns = SaveManager.LoadInfoFromFile(FileName.randomPuzzle).columns;
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
    }

    public void SaveBoard()
    {
        SaveManager.SaveInfo(FileName.randomPuzzle, board, rows, columns);
    }

    public void DeleteSavedBoard()
    {
        SaveManager.DeleteProgess(FileName.randomPuzzle);
    }

    public (string[,] board, int rows, int columns) GetSavedBoardData()
    {
        LoadSavedBoard();
        return (board, rows, columns);
    }
}

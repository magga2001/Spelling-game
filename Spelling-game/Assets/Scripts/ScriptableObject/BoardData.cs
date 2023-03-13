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
    [HideInInspector][SerializeField] private string Id;
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    public string ID { get { return Id; } set { Id = value; } }
    public int Rows { get { return rows; } set { rows = value; } }
    public int Columns { get { return columns; } set { columns = value; } }

    [HideInInspector][SerializeField] private List<Cell> board;

    [SerializeField] private List<string> words;
    public List<string> Words { get { return words; } set { words = value; } }

    public void CreateNewBoard()
    {
        board = new();

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                board.Add(new(i, j, ""));
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
        var cell = board.Find((e) => e.Column == column && e.Row == row);
        cell.Val = data;
    }

    public List<Cell> GetBoardData()
    {
        return board;
    }

    public Cell GetCell(int column, int row)
    {
        return board.Find((e) => e.Column == column && e.Row == row);
    }

    public void ClearBoard()
    {
        board.Clear();
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
    public (List<Cell> board, int rows , int columns) GetSavedBoardData()
    {
        LoadSavedBoard();
        return (board, rows, columns);
    }
}

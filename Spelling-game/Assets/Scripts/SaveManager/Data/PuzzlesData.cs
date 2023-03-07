using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[System.Serializable]
public class PuzzleData
{
    public string[,] board;
    public int rows;
    public int columns;

    public PuzzleData(string[,] board, int rows, int columns)
    {
        this.board = board;
        this.rows = rows;
        this.columns = columns;
    }
}

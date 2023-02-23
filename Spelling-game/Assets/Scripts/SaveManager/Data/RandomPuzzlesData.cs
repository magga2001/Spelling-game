using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomPuzzlesData
{
    public string[,] board;
    public int rows;
    public int columns;

    public RandomPuzzlesData(string[,] board, int rows, int columns)
    {
        this.board = board;
        this.rows = rows;
        this.columns = columns;
    }
}

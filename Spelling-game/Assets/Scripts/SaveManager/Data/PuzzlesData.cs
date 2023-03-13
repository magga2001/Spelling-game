using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[System.Serializable]
public class PuzzlesData
{
    public List<Cell> board;
    public int rows;
    public int columns;

    public PuzzlesData(List<Cell> board, int rows, int columns)
    {
        this.board = board;
        this.rows = rows;
        this.columns = columns;
    }
}

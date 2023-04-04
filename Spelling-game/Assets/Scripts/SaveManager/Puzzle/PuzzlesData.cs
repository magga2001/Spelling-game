using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[System.Serializable]
public class PuzzlesData
{
    [SerializeField] private List<Cell> board;
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    public PuzzlesData(List<Cell> board, int rows, int columns)
    {
        this.board = board;
        this.rows = rows;
        this.columns = columns;
    }

    public List<Cell> Board() => board;
    public int Rows() => rows;  
    public int Columns() => columns;
}

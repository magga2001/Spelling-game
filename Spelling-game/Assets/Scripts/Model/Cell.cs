using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cell
{
    [SerializeField] private int column;
    [SerializeField] private int row;
    [SerializeField] private string val;
    public int Column { get { return column; } set { column = value; } }
    public int Row { get { return row; } set { row = value; } }
    public string Val { get { return val; } set { val = value; } }

    public Cell(int column, int row, string val)
    {
        Column = column;
        Row = row;
        Val = val;
    }
}

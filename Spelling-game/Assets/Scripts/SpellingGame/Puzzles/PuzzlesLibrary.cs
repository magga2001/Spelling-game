using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PuzzlesLibrary : ScriptableObject
{
    [SerializeField] private List<BoardData> easyBoards = new List<BoardData>();
    [SerializeField] private List<BoardData> mediumBoards = new List<BoardData>();
    [SerializeField] private List<BoardData> hardBoards = new List<BoardData>();

    public List<BoardData> EasyBoards { get { return easyBoards; } set { easyBoards = value; } }
    public List<BoardData> MediumBoards { get { return mediumBoards; } set { mediumBoards = value; } }
    public List<BoardData> HardBoards { get { return hardBoards; } set { hardBoards = value; } }
}

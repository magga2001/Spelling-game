using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu]
public class PuzzlesManager : ScriptableObject
{
    [SerializeField] private Puzzles library;
    [SerializeField] private SpellingDifficultiesManager spellingDifficultiesManager;
    private Queue<BoardData> puzzles = new();
    private BoardData currentPuzzle;
    private Difficulties currentDifficulty;

    public void SetUp()
    {
        SetUpDifficulty(spellingDifficultiesManager.Difficulties);
    }

    public void SetUpDifficulty(Difficulties difficulties)
    {
        puzzles.Clear();

        var boards = new List<BoardData>();
        currentDifficulty = difficulties;

        switch (difficulties)
        {
            case Difficulties.EASY:
                boards = library.EasyBoards;
                break;
            case Difficulties.MEDIUM:
                boards = library.MediumBoards;
                break;
            case Difficulties.HARD:
                boards = library.HardBoards;
                break;
        }

        Shuffle(boards);

        foreach (var board in boards)
        {
            puzzles.Enqueue(board);
        }

        DequeueBoard();
    }

    public void NextBoard()
    {
        if (puzzles.Count != 0)
        {
            DequeueBoard();
        }
        else
        {
            SetUpDifficulty(spellingDifficultiesManager.Difficulties);
        }
    }

    private void DequeueBoard()
    {
        var board = puzzles.Dequeue();

        currentPuzzle = board;
    }

    public BoardData GetCurrentPuzzle()
    {
        return currentPuzzle;
    }

    public void UpdatePuzzlesDifficulty()
    {
        if(currentDifficulty != spellingDifficultiesManager.Difficulties)
        {
            SetUpDifficulty(spellingDifficultiesManager.Difficulties);
        }
    }

    private void Shuffle(List<BoardData> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            Random rnd = new();
            int i = rnd.Next(n + 1);
            (list[n], list[i]) = (list[i], list[n]);
        }
    }
}

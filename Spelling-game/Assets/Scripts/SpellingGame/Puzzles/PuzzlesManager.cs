using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu]
public class PuzzlesManager : ScriptableObject
{
    [SerializeField] private PuzzlesLibrary library;
    [SerializeField] private SpellingDifficultiesManager spellingDifficultiesManager;
    private Queue<BoardData> puzzles = new();
    private BoardData currentPuzzle;
    private Difficulties currentDifficulty;

    //Set up the difficulty at the start of the game session
    public void SetUp()
    {
        SetUpDifficulty(spellingDifficultiesManager.Difficulties);
    }

    //Load up all the words from puzzles library and add to the queue for the game session
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

        Randomize(boards);

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
            //If the game is endless, then go to next difficulty
            //Otherwise stay the same difficulty
            if (GameManager.Instance.IsEndless)
            {
                spellingDifficultiesManager.PromoteDifficulty();
            }
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

    private void Randomize(List<BoardData> inputList)
    {
        int listSize = inputList.Count;
        while (listSize > 1)
        {
            listSize--;
            Random randomGenerator = new();
            int randomIndex = randomGenerator.Next(listSize + 1);
            (inputList[listSize], inputList[randomIndex]) = (inputList[randomIndex], inputList[listSize]);
        }
    }
}

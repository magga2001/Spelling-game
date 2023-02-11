using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AlphabetData;

public class WordGrid : MonoBehaviour
{
    [SerializeField] private BoardData boardData;
    [SerializeField] private AlphabetData alphabetData;
    [SerializeField] private List<GameObject> letters;

    private string[,] board;
    private int rows;
    private int columns;

    void Awake()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            InstantiateWordGrid();
        }
    }

    public void InstantiateWordGrid()
    {
        board = boardData.GetSavedBoardData().board;
        rows = boardData.GetSavedBoardData().rows;
        columns = boardData.GetSavedBoardData().columns;

        //Build the board here
        for (int i = 0; i < columns; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                var letter_default = alphabetData.Default_alphabets.Find(e => e.Alphabet == board[i, j]);
                var letter_selected = alphabetData.Selected_alphabets.Find(e => e.Alphabet == board[i, j]);
                var letter_correct = alphabetData.Correct_alphabets.Find(e => e.Alphabet == board[i, j]);
                var letter_wrong = alphabetData.Wrong_alphabets.Find(e => e.Alphabet == board[i, j]);

                if (letter_default != null && letter_selected != null && letter_correct != null && letter_wrong != null)
                {
                    letters.Add(LetterObjectPoolingManager.Instance.GetLetter(
                        letter_default.Alphabet,
                        letter_default.Image,
                        letter_selected.Image,
                        letter_correct.Image,
                        letter_wrong.Image));
                }
            }
        }
    }
}

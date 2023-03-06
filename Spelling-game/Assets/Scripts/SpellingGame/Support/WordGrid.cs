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
    [SerializeField] private List<string> words;
    [SerializeField] private Camera cam;
    [SerializeField] private float margin;

    private string[,] board;
    private int rows;
    private int columns;
    [SerializeField] private List<GameObject> wordBoxes;

    public string[,] Board { get { return board; } set { board = value; } }
    public int Rows { get { return rows; } set { rows = value; } }
    public int Columns { get { return columns; } set { columns = value; } }

    public List<string> Words { get { return words; } set { words = value; } }
    public List<GameObject> WordBoxes { get { return wordBoxes; } set { wordBoxes = value; } }

    private void OnEnable()
    {
        ClearBoard();   
        InstantiateWordGrid();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            InstantiateWordGrid();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PuzzleSaveManager.DeleteProgess(FileName.puzzle);
        }
    }

    public void InstantiateWordGrid()
    {
        board = boardData.GetSavedBoardData().board;
        rows = boardData.GetSavedBoardData().rows;
        columns = boardData.GetSavedBoardData().columns;
        words = boardData.Words;

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
                        letter_wrong.Image, i, j));
                }
            }
        }

        foreach (var word in words)
        {
            wordBoxes.Add(WordObjectPoolingManager.Instance.GetWord(word));
        }

        PlaceGridOnScreen();

        if(wordBoxes.Count > 0)
        {
            PlaceWordBoxOnScreen();
        }
    }

    private void PlaceGridOnScreen()
    {
        Vector3 centerPos = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));
        float offset = letters[0].transform.localScale.x + margin;
        float localRowOffset = 0f;
        float localColOffset = 0f;
        int col = 0;

        foreach (var letter in letters)
        {
            letter.transform.position = centerPos + new Vector3(-offset * rows/2 + localRowOffset, offset * columns/2 - localColOffset, 0f);
            col++;
            if (col >= Rows)
            {
                col = 0;
                localColOffset = 0f;
                localRowOffset += offset;
            }
            else
            {   
                localColOffset += offset;
            }
        }
    }

    private void PlaceWordBoxOnScreen()
    {
        Vector3 sidePos = cam.ViewportToWorldPoint(new Vector3(0.8f, 0.3f, 10f));
        float offset = wordBoxes[0].transform.localScale.x + 0.25f;

        int maxPerRow = 3;
        int maxPerCol = 3;
        float localRowOffset = 0f;
        float localColOffset = 0f;
        int row = 0;

        foreach(var word in wordBoxes)
        {
            word.transform.position = sidePos + new Vector3(-offset * maxPerRow / 2 + localRowOffset, offset * maxPerCol / 2 - localColOffset, 0f);
            row++;
            if (row >= maxPerRow)
            {
                row = 0;
                localRowOffset = 0f;
                localColOffset += offset;
            }
            else
            {
                localRowOffset += offset;
            }
        }
    }

    public void ClearBoard()
    {
        letters.Clear();
        wordBoxes.Clear();
    }

    private void OnDisable()
    {
        foreach(var letter in letters)
        {
            if(letter != null)
            {
                letter.SetActive(false);
            }
        }

        foreach (var wordBox in wordBoxes)
        {
            if (wordBox != null)
            {
                wordBox.SetActive(false);
            }
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGrid : MonoBehaviour
{
    [SerializeField] private PuzzlesManager pm;
    [SerializeField] private BoardData boardData;
    [SerializeField] private AlphabetData alphabetData;
    [SerializeField] private List<GameObject> letters;
    [SerializeField] private List<string> words;
    [SerializeField] private Camera cam;
    [SerializeField] private float margin;

    private List<Cell> board;
    private int rows;
    private int columns;
    [SerializeField] private List<GameObject> wordBoxes;

    public List<Cell> Board { get { return board; } set { board = value; } }
    public int Rows { get { return rows; } set { rows = value; } }
    public int Columns { get { return columns; } set { columns = value; } }

    public List<string> Words { get { return words; } set { words = value; } }
    public List<GameObject> WordBoxes { get { return wordBoxes; } set { wordBoxes = value; } }

    private void OnEnable()
    {
        ResetBoard();
        boardData = pm.GetCurrentPuzzle();
        InstantiateWordGrid();
    }

    public void NextPuzzle()
    {
        ClearBoard();
        ResetBoard();
        pm.NextBoard();
        boardData = pm.GetCurrentPuzzle();
        InstantiateWordGrid();
    }

    //Part of this code function is taken and from
    //CodePlanStudio, C. (Director). (2020, August 07). Words Spy Game Episode 7 | Unity Word Searching Game [Video file]. Retrieved February 13, 2023, from https://www.youtube.com/watch?v=GrhOUbPiLpM&amp;list=PLJLLSehgFnspMBk7VaLI18Digsj2xuMhT&amp;index=7
    public void InstantiateWordGrid()
    {
        rows = boardData.Rows;
        columns = boardData.Columns;
        words = boardData.Words;

        //Build the board here
        for (int i = 0; i < columns; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                //Fetch all the states of the particular alphabets
                var letter_default = alphabetData.Default_alphabets.Find(e => e.Alphabet == boardData.GetCell(i, j).Val);
                var letter_selected = alphabetData.Selected_alphabets.Find(e => e.Alphabet == boardData.GetCell(i, j).Val);
                var letter_correct = alphabetData.Correct_alphabets.Find(e => e.Alphabet == boardData.GetCell(i, j).Val);
                var letter_wrong = alphabetData.Wrong_alphabets.Find(e => e.Alphabet == boardData.GetCell(i, j).Val);

                //Apply it to letter box to customise the alphabet onto the letter box game object that builds up the board
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

    //Utilising the spelling camera to always have the board be instantiated in the centre of the camera
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

    //Utilising the spelling camera to always have the all the word lists be instantiated
    //in the right part of the camera

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

    public void ResetBoard()
    {
        letters.Clear();
        wordBoxes.Clear();
    }

    //Clear all the board data and alphabet data, so that it can be reused
    public void ClearBoard()
    {
        foreach (var letter in letters)
        {
            if (letter != null)
            {
                letter.GetComponent<LetterBox>().SetDefaultSprite();
                letter.GetComponent<LetterBox>().Founded = false;   
                letter.SetActive(false);
            }
        }

        foreach (var wordBox in wordBoxes)
        {
            if (wordBox != null)
            {
                wordBox.GetComponent<WordBox>().SetDefault();
                wordBox.GetComponent<WordBox>().Founded = false;
                wordBox.SetActive(false);
            }
        }
    }

    public void HideBoard()
    {
        foreach (var letter in letters)
        {
            if (letter != null)
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

    private void OnDisable()
    {
        HideBoard();
        ResetBoard();
    }

}

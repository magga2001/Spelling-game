using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AlphabetData;

public class WordSearchGame : Subject<(PlayerAction, PlayerAnswerData)>
{
    private Stack<GameObject> currentWordOrder = new Stack<GameObject>();

    [SerializeField] private WordGrid currentWordGrid;
    [SerializeField] private Camera cam;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] private List<string> wordsFounded = new List<string>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // To find where the mouse is being clicked to which game object
            // In this case, it is to find which letters has been clicked on the board
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("LetterBox"))
                {
                    Main(hit.transform.gameObject, hit.transform.GetComponent<LetterBox>());
                }
            }
        }

        if (GameManager.Instance.GameIsOver)
        {
            gameObject.SetActive(false);
        }
    }

    //Manage if the letter should be added or removed in the constructed word order by the player
    private void Main(GameObject letter, LetterBox letterBox)
    {
        if (CheckIfMatchesRecentAlphabet(letter) && letterBox.Selected)
        {
            RemoveRecentAlphabet();
            letterBox.Selected = !letterBox.Selected;
            if(letterBox.Founded)
            {
                letterBox.SetCorrectSprite();
            }
            else
            {
                letterBox.SetDefaultSprite();
            }

            AudioManager.instance.Play("SelectAlphabet");
        }

        else if (!letterBox.Selected)
        {
           AddAlphabetToWordOrder(letter, letterBox);
        }

        ConstructLine();
    }

    // Add letter to the constructed word order
    public void AddAlphabetToWordOrder(GameObject alphabet, LetterBox letterBox)
    {
        int wordOrderLength = currentWordOrder.Count;

        //Check if none of the letter is selected yet on the board, any letters can be selected
        if (wordOrderLength == 0)
        {
            currentWordOrder.Push(alphabet);
            letterBox.Selected = !letterBox.Selected;
            letterBox.SetSelectedSprite();
            AudioManager.instance.Play("SelectAlphabet");
        }
        //Check if only one letter is selected on the board, allowing any direction for next selection of the letter
        else if (wordOrderLength == 1 && GetFirstLegalSelection(currentWordOrder.Peek().GetComponent<LetterBox>().Position).Contains(alphabet.GetComponent<LetterBox>().Position))
        {
            currentWordOrder.Push(alphabet);
            letterBox.Selected = !letterBox.Selected;
            letterBox.SetSelectedSprite();
            AudioManager.instance.Play("SelectAlphabet");
        }
        //Check if two or more letters are selected on the board, allowing one legal direction for next selection of the letter
        else if (wordOrderLength >= 2 && GetLegalSelection(currentWordOrder.Peek().GetComponent<LetterBox>().Position) == alphabet.GetComponent<LetterBox>().Position)
        {
            currentWordOrder.Push(alphabet);
            letterBox.Selected = !letterBox.Selected;
            letterBox.SetSelectedSprite();
            AudioManager.instance.Play("SelectAlphabet");
        }
        else
        {
            AudioManager.instance.Play("Non-legal");
            Debug.Log("No legal moves");
        }
    }

    // Remove letter to the constructed word order
    public void RemoveRecentAlphabet()
    {
        if(currentWordOrder.Count > 0)
        {
            currentWordOrder.Pop();
        }
    }

    //Check if the most recent added letter is being clicked
    //This restrict to allowing only the removal of the latest letter in the constructed word order
    public bool CheckIfMatchesRecentAlphabet(GameObject alphabet)
    {
        if (currentWordOrder.Count == 0)
        {
            return false;
        }

        return ReferenceEquals(currentWordOrder.Peek(), alphabet);
    }

    // Clear the word order
    public void ResetWordOrder()
    {
        currentWordOrder.Clear();
    }

    //if only one letter is selected on the board, check that the current letter that
    //is being clicked is next to the latest selected letter in any direction
    public List<(int,int)> GetFirstLegalSelection((int col, int row) position)
    {
        List<(int,int)> selections = new List<(int,int)>();

        int col = position.col;
        int row = position.row; 

        selections.Add((col, row - 1));
        selections.Add((col + 1, row - 1));
        selections.Add((col + 1, row));
        selections.Add((col + 1, row + 1));
        selections.Add((col, row + 1));
        selections.Add((col - 1, row + 1));
        selections.Add((col - 1, row));
        selections.Add((col - 1, row - 1));

        return selections.FindAll(e => IsLegal(e));
    }

    //if multiple letters are selected on the board, check that the current letter that
    //is being clicked is next to the latest selected letter in one direction
    public (int, int) GetLegalSelection((int col, int row) position)
    {
        int col = position.col;
        int row = position.row;

        List<GameObject> currentWordOrderList = currentWordOrder.ToList();
        currentWordOrderList.Reverse();

        var lastItem = currentWordOrderList[currentWordOrderList.Count - 1].GetComponent<LetterBox>().Position;
        var preLastitem = currentWordOrderList[currentWordOrderList.Count - 2].GetComponent<LetterBox>().Position;

        int magnitude_col = lastItem.col - preLastitem.col;
        int magnitude_row = lastItem.row - preLastitem.row;

        if (magnitude_col < 0 && magnitude_row < 0 && IsLegal((col - 1, row - 1)))
        {
           return (col - 1, row - 1);
        }
        if (magnitude_col > 0 && magnitude_row > 0 && IsLegal((col + 1, row + 1)))
        {
            return (col + 1, row + 1);
        }
        if (magnitude_col == 0 && magnitude_row < 0 && IsLegal((col, row - 1)))
        {
            return (col, row - 1);
        }
        if (magnitude_col < 0 && magnitude_row == 0 && IsLegal((col - 1, row)))
        {
            return (col - 1, row);
        }
        if (magnitude_col == 0 && magnitude_row > 0 && IsLegal((col, row + 1)))
        {
            return (col, row + 1);
        }
        if (magnitude_col > 0 && magnitude_row == 0 && IsLegal((col + 1, row)))
        {
            return (col + 1, row);
        }
        if (magnitude_col > 0 && magnitude_row < 0 && IsLegal((col + 1, row - 1)))
        {
            return (col + 1, row - 1);
        }
        if (magnitude_col < 0 && magnitude_row > 0 && IsLegal((col - 1, row + 1)))
        {
            return (col - 1, row + 1);
        }

        return (col, row);
    }

    //Return all the legal cell on the board
    private bool IsLegal((int col, int row) selection)
    {
        return selection.col >= 0 && selection.row >= 0 && selection.col <= currentWordGrid.Columns && selection.row <= currentWordGrid.Rows;
    }

    //Construct a line renderer to show the the direction of the word order
    //Line will only appear when two or more letters are selected
    private void ConstructLine()
    {
        if(currentWordOrder.Count > 0)
        {
            List<GameObject> currentWordOrderList = currentWordOrder.ToList();
            currentWordOrderList.Reverse();

            lineRenderer.SetPosition(0, currentWordOrderList[0].transform.position);
            lineRenderer.startWidth = 0.15f;
            lineRenderer.endWidth = 0.15f;
            lineRenderer.SetPosition(1, currentWordOrderList[currentWordOrderList.Count - 1].transform.position);
        }
        else
        {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }
    }

    public void CheckAnswer()
    {
        if (currentWordOrder.Count > 0)
        {
            List<GameObject> currentWordOrderList = currentWordOrder.ToList();
            currentWordOrderList.Reverse();

            string currentWord = "";

            foreach (var word in currentWordOrderList)
            {
                currentWord += word.GetComponent<LetterBox>().Letter;
            }

            //Check if the word order answer submitted by the player is one of the words that needed to be found
            if (currentWordGrid.Words.Contains(currentWord) && !wordsFounded.Contains(currentWord))
            {
                wordsFounded.Add(currentWord);
                var word = currentWordGrid.WordBoxes.Find(e => e.GetComponent<WordBox>().Word == currentWord);
                word.GetComponent<WordBox>().WordFounded();
                SetCorrectLetterBox();
                NotifyObservers((PlayerAction.SPELLED_CORRECT, new(SpellingGames.WORDSEARCH ,word.GetComponent<WordBox>().Word, currentWord)));
                ResetWordOrder();
                ConstructLine();
                NextPuzzle();
            }
            else
            {
                NotifyObservers((PlayerAction.SPELLED_WRONG, new(SpellingGames.WORDSEARCH, "", currentWord)));
            }
        }
    }

    //To allow overlapping in letters
    private void SetCorrectLetterBox()
    {
        foreach(var letterBox in currentWordOrder)
        {
            letterBox.GetComponent<LetterBox>().SetCorrectSprite();
            letterBox.GetComponent<LetterBox>().Founded = true;
            letterBox.GetComponent<LetterBox>().Selected = false;
        }
    }

    //Check all the word that is all founded;
    private void NextPuzzle()
    {
        foreach(var word in currentWordGrid.WordBoxes)
        {
            if (!wordsFounded.Contains(word.GetComponent<WordBox>().Word))
            {
                return;
            };
        }
        wordsFounded.Clear();
        currentWordGrid.NextPuzzle();
    }

    private void OnDisable()
    {
        ResetWordOrder();
        ConstructLine();
    }
}

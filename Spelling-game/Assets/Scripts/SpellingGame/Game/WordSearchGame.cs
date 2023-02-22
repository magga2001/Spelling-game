using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSearchGame : MonoBehaviour
{
    private Stack<GameObject> currentWordOrder = new Stack<GameObject>();

    [SerializeField] private WordGrid currentWordGrid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAlphabetToWordOrder(GameObject alphabet)
    {
        if (GetLegalSelection(currentWordOrder.Peek().GetComponent<LetterBox>().Position).Contains(alphabet.GetComponent<LetterBox>().Position))
        {
            currentWordOrder.Push(alphabet);
        }
        else
        {
            Debug.Log("No legal moves");
        }
    }

    public void RemoveRecentAlphabet()
    {
        if(currentWordOrder.Count > 0)
        {
            currentWordOrder.Pop();
        }
    }

    public bool CheckIfMatchesRecentAlphabet(GameObject alphabet)
    {
        return GameObject.ReferenceEquals(currentWordOrder.Peek(), alphabet);
    }

    public void ResetWordOrder()
    {
        currentWordOrder.Clear();
    }

    public List<(int,int)> GetLegalSelection((int col, int row) position)
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

    private bool IsLegal((int col, int row) selection)
    {
        return selection.col >= 0 && selection.row >= 0 && selection.col <= currentWordGrid.Columns && selection.row <= currentWordGrid.Rows;
    }
}

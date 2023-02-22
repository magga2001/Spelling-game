using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WordSearchGame : MonoBehaviour
{
    private Stack<GameObject> currentWordOrder = new Stack<GameObject>();

    [SerializeField] private WordGrid currentWordGrid;
    [SerializeField] private Camera cam;
    [SerializeField] LineRenderer lineRenderer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("LetterBox"))
                {
                    Main(hit.transform.gameObject, hit.transform.GetComponent<LetterBox>());
                }
            }
        }
    }

    private void Main(GameObject letter, LetterBox letterBox)
    {
        if (CheckIfMatchesRecentAlphabet(letter) && letterBox.Selected)
        {
            RemoveRecentAlphabet();
        }

        if (!letterBox.Selected)
        {
           AddAlphabetToWordOrder(letter);
        }

        letterBox.Selected = !letterBox.Selected;

        ConstructLine();
    }

    public void AddAlphabetToWordOrder(GameObject alphabet)
    {
        if(currentWordOrder.Count == 0)
        {
            currentWordOrder.Push(alphabet);
        }
        else if (GetLegalSelection(currentWordOrder.Peek().GetComponent<LetterBox>().Position).Contains(alphabet.GetComponent<LetterBox>().Position))
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
        if (currentWordOrder.Count == 0)
        {
            return false;
        }

        return ReferenceEquals(currentWordOrder.Peek(), alphabet);
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

    private void ConstructLine()
    {
        List<GameObject> currentWordOrderList = currentWordOrder.ToList();

        lineRenderer.SetPosition(0, currentWordOrderList[0].transform.position);
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineRenderer.SetPosition(1, currentWordOrderList[currentWordOrderList.Count - 1].transform.position);
    }
}

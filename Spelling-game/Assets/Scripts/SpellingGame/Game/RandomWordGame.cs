using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomWordGame : MonoBehaviour
{
    private Stack<GameObject> currentWordOrder = new Stack<GameObject>();

    [SerializeField] private RandomWordGrid currentWordGrid;
    [SerializeField] private Camera cam;
    [SerializeField] private AnswerBox answerBox;

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
            Debug.Log("Removing");
            RemoveRecentAlphabet();
            letterBox.Selected = !letterBox.Selected;
        }

        else if (!letterBox.Selected)
        {
            AddAlphabetToWordOrder(letter);
            letterBox.Selected = !letterBox.Selected;
        }
    }

    public void AddAlphabetToWordOrder(GameObject alphabet)
    {
        currentWordOrder.Push(alphabet);

        UpdateAnswerBox();
    }

    public void RemoveRecentAlphabet()
    {
        if (currentWordOrder.Count > 0)
        {
            currentWordOrder.Pop();
            UpdateAnswerBox();
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
        foreach (var letter in currentWordOrder)
        {
            letter.GetComponent<LetterBox>().Selected = false;
        }
        currentWordOrder.Clear();
        UpdateAnswerBox();
    }

    public void ConfirmWord()
    {
        var word = GetCurrentWord();
    }

    private string GetCurrentWord()
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

            return currentWord;
        }

        return "";
    }

    private void UpdateAnswerBox()
    {
        answerBox.Word = GetCurrentWord();
        answerBox.SetAnswerBoxes();
    }
}

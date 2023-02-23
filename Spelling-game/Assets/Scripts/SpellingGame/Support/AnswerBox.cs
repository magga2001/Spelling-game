using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnswerBox : MonoBehaviour
{
    private string word;
    public string Word { get { return word; } set { word = value; } }

    [SerializeField] private TextMeshPro text;

    public void SetAnswerBoxes()
    {
        text.text = word;
    }

    public void WordFounded()
    {
        //gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }
}

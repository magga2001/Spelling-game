using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordBox : MonoBehaviour
{
    private string word;
    public string Word { get { return word; } set { word = value; } }

    [SerializeField] private TextMeshPro text;

    public void SetWordBoxes()
    {
        text.text = word;
    }

    public void WordFounded()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordBox : MonoBehaviour
{
    private string word;
    public string Word { get { return word; } set { word = value; } }

    private bool founded;
    public bool Founded { get { return founded; } set { founded = value; } }

    [SerializeField] private TextMeshPro text;

    public void SetWordBoxes()
    {
        text.text = word;
    }

    public void WordFounded()
    {
        founded = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void SetDefault()
    {
        founded = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}

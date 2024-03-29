using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : MonoBehaviour
{
    private Sprite default_alphabet;

    private Sprite selected_alphabet;

    private Sprite correct_alphabet;

    private Sprite wrong_alphabet;


    private string letter;
    private (int, int) position;
    public string Letter { get { return letter; } set { letter = value; } }
    public (int col, int row) Position { get { return position; } set { position = value; } }

    private bool selected;
    public bool Selected { get { return selected; } set { selected = value; } }

    private bool founded;
    public bool Founded { get { return founded; } set { founded = value; } }

    public void SetLetter(string letter)
    {
        this.letter = letter;
    }

    public void SetSprites(
        Sprite default_alphabet,
        Sprite selected_alphabet,
        Sprite correct_alphabet,
        Sprite wrong_alphabet)
    {
        this.default_alphabet = default_alphabet;
        this.selected_alphabet = selected_alphabet;
        this.correct_alphabet = correct_alphabet;
        this.wrong_alphabet = wrong_alphabet;
    }

    public void SetDefaultSprite()
    {
        this.GetComponent<SpriteRenderer>().sprite = default_alphabet;
    }

    public void SetSelectedSprite()
    {
        this.GetComponent<SpriteRenderer>().sprite = selected_alphabet;
    }

    public void SetCorrectSprite()
    {
        this.GetComponent<SpriteRenderer>().sprite = correct_alphabet;
    }

    public void SetPosition(int col, int row)
    {
        position = (col, row);
    }
}

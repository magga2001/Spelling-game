using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : MonoBehaviour
{
    private Sprite currentSprite;

    private Sprite default_alphabet;

    private Sprite selected_alphabet;

    private Sprite correct_alphabet;

    private Sprite wrong_alphabet;

    private WordSearchGame wordSearchGame;

    private string letter;
    private (int, int) position;
    public string Letter { get { return letter; } set { letter = value; } }
    public (int, int) Position { get { return position; } set { position = value; } }

    private bool selected;

    private bool disabled;

    private void OnEnable()
    {
        selected = false;
        disabled = false;

       // Still have to assign 
       // wordSearchGame = GameObject.Fin
    }

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

        currentSprite = this.default_alphabet;

        //Add LETTER IMAGE FIRST
        //this.GetComponent<SpriteRenderer>().sprite = currentSprite;
    }

    public void SetPosition(int col, int row)
    {
        position = (col, row);
    }

    public void UpdateCurrentSprite()
    {
        //TODO
    }

    private void OnMouseDown()
    {

        if (wordSearchGame.CheckIfMatchesRecentAlphabet(this.gameObject) && selected)
        {
            wordSearchGame.RemoveRecentAlphabet();  
        }

        if(!selected)
        {
            wordSearchGame.AddAlphabetToWordOrder(this.gameObject);
        }

        selected = !selected;
    }
}

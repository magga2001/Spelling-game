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


    private string letter;  
    public string Letter { get { return letter; } set { letter = value; } }
   
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

    public void UpdateCurrentSprite()
    {
        //TODO
    }
}

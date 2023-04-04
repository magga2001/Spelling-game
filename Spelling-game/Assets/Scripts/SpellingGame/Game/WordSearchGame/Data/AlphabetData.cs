using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The code is inspired and taken from
//CodePlanStudio, C. (2020, August 02). Words spy game episode 6 | unity word searching game. Retrieved February 12, 2023, from https://www.youtube.com/watch?v=r4MFdbkIM0M&list=PLJLLSehgFnspMBk7VaLI18Digsj2xuMhT&index=6
//This class is to store all the English alphabet states to use for Word search puzzles
[System.Serializable]
[CreateAssetMenu]
public class AlphabetData : ScriptableObject    
{
    [System.Serializable]
    public class Letter
    {
        [SerializeField] private string alphabet;
        [SerializeField] private Sprite image;
        public string Alphabet { get { return alphabet; } set { alphabet = value; } }
        public Sprite Image { get { return image; } set { image = value; } }

    }
    //First state of the alphabets
    [SerializeField] private List<Letter> default_alphabets = new();
    //When the alphabet is selected
    [SerializeField] private List<Letter>  selected_alphabets = new();
    //When the answer is correct, then the alphabets in the answer turns this state
    [SerializeField] private List<Letter> correct_alphabets = new();
    //When the answer is wrong, then alphabets in the answer turns this state
    [SerializeField] private List<Letter> wrong_alphabets = new();

    public List<Letter> Default_alphabets { get { return default_alphabets; } set { default_alphabets = value; } }
    public List<Letter> Selected_alphabets { get { return selected_alphabets; } set { selected_alphabets = value; } }
    public List<Letter> Correct_alphabets { get { return correct_alphabets; } set { correct_alphabets = value; } }
    public List<Letter> Wrong_alphabets { get { return wrong_alphabets; } set { wrong_alphabets = value; } }

}

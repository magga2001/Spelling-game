using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private List<Letter> default_alphabets = new();
    [SerializeField] private List<Letter>  selected_alphabets = new();
    [SerializeField] private List<Letter> correct_alphabets = new();
    [SerializeField] private List<Letter> wrong_alphabets = new();

    public List<Letter> Default_alphabets { get { return default_alphabets; } set { default_alphabets = value; } }
    public List<Letter> Selected_alphabets { get { return selected_alphabets; } set { selected_alphabets = value; } }
    public List<Letter> Correct_alphabets { get { return correct_alphabets; } set { correct_alphabets = value; } }
    public List<Letter> Wrong_alphabets { get { return wrong_alphabets; } set { wrong_alphabets = value; } }

}

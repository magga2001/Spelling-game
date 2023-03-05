using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int highScore;
    public List<string> correctWords;
    public List<string> IncorrectWords;   
    public PlayerData(int highScore, List<string> correctWords, List<string> IncorrectWords)
    {
        this.highScore = highScore;
        this.correctWords = correctWords;   
        this.IncorrectWords = IncorrectWords;   
    }
}

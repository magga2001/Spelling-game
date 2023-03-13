using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<HighScoreData> highScores;
    public List<string> CorrectWords;
    public List<string> IncorrectWords;   
    public PlayerData(List<HighScoreData> highScores, List<string> correctWords, List<string> IncorrectWords)
    {
        this.highScores = highScores;
        this.CorrectWords = correctWords;   
        this.IncorrectWords = IncorrectWords;   
    }
}

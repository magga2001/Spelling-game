using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<HighScoreData> highScores;
    public List<string> correctWords;
    public List<string> IncorrectWords;   
    public PlayerData(List<HighScoreData> highScores, List<string> correctWords, List<string> IncorrectWords)
    {
        this.highScores = highScores;
        this.correctWords = correctWords;   
        this.IncorrectWords = IncorrectWords;   
    }
}

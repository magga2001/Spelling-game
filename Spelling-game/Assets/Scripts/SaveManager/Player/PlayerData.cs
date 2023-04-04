using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField] private List<HighScoreData> highScores;
    [SerializeField] private List<string> correctWords;
    [SerializeField] private List<string> incorrectWords;   
    public PlayerData(List<HighScoreData> highScores, List<string> correctWords, List<string> IncorrectWords)
    {
        this.highScores = highScores;
        this.correctWords = correctWords;   
        this.incorrectWords = IncorrectWords;   
    }

    public List<HighScoreData> HighScores() => highScores;
    public List<string> CorrectWords() => correctWords;
    public List<string> IncorrectWords() => incorrectWords;
}

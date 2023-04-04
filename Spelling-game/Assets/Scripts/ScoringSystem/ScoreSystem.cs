using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class ScoreSystem : ScriptableObject
{
    [SerializeField] private int scale;

    [HideInInspector][SerializeField] private int highScore;
    [HideInInspector][SerializeField] private int score;

    //The score is set up to to 0 for new game session
    //The high scores for particular game mode, type and difficulties will also be fetched from save file
    public void SetUp(GameMode gameMode)
    {
        score = 0;

        PlayerData data = PlayerSaveManager.LoadInfo();
        try
        {
            var highScoreData = data.HighScores().Find((e) => e.Game == gameMode.Game && e.Difficulties == gameMode.Difficulties && GameManager.Instance.IsEndless());

            if (highScoreData != null)
            {
                highScore = highScoreData.Score;
            }
            else
            {
                highScore = 0;
            }
        }
        catch (Exception)
        {
            highScore = 0;
            Debug.Log("Score File not found");
        }
    }

    public void IncreaseScore(int newScore)
    {
        score += newScore * scale;
    }

    public void DecreaseScore(int newScore)
    {
        score -= newScore * scale;
        score = Math.Max(score, 0);
    }

    public int GetUpdateHighScore()
    {
        highScore = Math.Max(score, highScore);
        return highScore;
    }

    public int GetScore() => score;
    public int GetHighScore() => highScore; 
}

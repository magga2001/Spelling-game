using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class ScoreSystem : ScriptableObject
{
    [SerializeField] private int scale;

    private int highScore;
    private int score;
    public void SetUp()
    {
        score = 0;

        PlayerData data = PlayerSaveManager.LoadPlayerInfo();
        try
        {
            highScore = data.highScore;


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

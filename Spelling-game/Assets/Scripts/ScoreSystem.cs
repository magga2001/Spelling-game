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
            Debug.Log("File not found");
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

    public void UpdateHighScore()
    {
        highScore = Math.Max(score, highScore);
    }

    public int GetScore() => score;
    public int GetHighScore() => highScore; 
}

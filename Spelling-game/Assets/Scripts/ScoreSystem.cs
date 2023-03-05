using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int scale;
    [SerializeField] private TextMeshProUGUI scoreUI;

    private int highScore;
    private int score;
    void Awake()
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
        scoreUI.text = score.ToString();
    }

    public void DecreaseScore(int newScore)
    {
        score -= newScore * scale;
        score = Math.Max(score, 0);
        scoreUI.text = score.ToString();
    }

    public void UpdateHighScore()
    {
        highScore = Math.Max(score, highScore);
    }

    public int GetScore() => score;
    public int GetHighScore() => highScore; 
}

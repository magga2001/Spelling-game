using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int scale;
    [SerializeField] private TextMeshProUGUI scoreUI;

    private int score;
    void Awake()
    {
        score = 0;
    }

    public void IncreaseScore(int newScore)
    {
        Debug.Log(newScore);
        score += newScore * scale;
        scoreUI.text = score.ToString();
    }

    public void DecreaseScore(int newScore)
    {
        Debug.Log(newScore);
        score -= newScore * scale;
        if(score <= 0)
        {
            score = 0;
        }
        scoreUI.text = score.ToString();
    }

    public int GetScore() => score;
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private static ScoreSystem instance;
    public static ScoreSystem Instance { get { return instance; } }

    [SerializeField] private Weapon playerWeapon;

    [SerializeField] private TextMeshProUGUI scoreUI;

    private int score;
    void Awake()
    {
        instance = this;
        score = 0;
    }

    public void IncreaseScore(int newScore)
    {
        //playerWeapon.Fire();
        score += newScore;
        scoreUI.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}

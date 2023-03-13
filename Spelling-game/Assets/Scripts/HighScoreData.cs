using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreData
{
    private SpellingGames game;
    private Difficulties difficulties;
    private int score;
    private bool isEndless;

    public HighScoreData(SpellingGames game, Difficulties difficulties, int score, bool isEndless)
    {
        this.game = game;
        this.difficulties = difficulties;
        this.score = score;
        this.isEndless = isEndless;
    }

    public SpellingGames Game { get { return game; } set { game = value; } }
    public Difficulties Difficulties { get { return difficulties; } set { difficulties = value; } }
    public int Score { get { return score; } set { score = value; } }
    public bool IsEndless { get { return isEndless; } set { isEndless = value; } }
}


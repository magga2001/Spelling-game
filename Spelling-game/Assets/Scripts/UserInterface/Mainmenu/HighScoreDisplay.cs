using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown gameModeDropdown;

    [SerializeField] private TextMeshProUGUI easyScore;
    [SerializeField] private TextMeshProUGUI mediumScore;
    [SerializeField] private TextMeshProUGUI hardScore;
    [SerializeField] private TextMeshProUGUI endlessScore;

    [SerializeField] private List<HighScoreData> highScores;

    private void Start()
    {
        LoadScores();
        DisplayHighScores();
    }

    //Displaying high scores for different game mode
    public void DisplayHighScores()
    {
        LoadScores();

        var gameMode = GetGameMode();

        var hasEasy = HasHighScores(gameMode, Difficulties.EASY);
        var hasMedium = HasHighScores(gameMode, Difficulties.MEDIUM);
        var hasHard = HasHighScores(gameMode, Difficulties.HARD);
        var isEndless = highScores.Contains(highScores.Find((e) => e.Game == gameMode && e.IsEndless == true));

        easyScore.text = hasEasy ? highScores.Find((e) => e.Game == gameMode && e.Difficulties == Difficulties.EASY && e.IsEndless == false).Score.ToString() : 0.ToString();
        mediumScore.text = hasMedium ? highScores.Find((e) => e.Game == gameMode && e.Difficulties == Difficulties.MEDIUM && e.IsEndless == false).Score.ToString() : 0.ToString();
        hardScore.text = hasHard ? highScores.Find((e) => e.Game == gameMode && e.Difficulties == Difficulties.HARD && e.IsEndless == false).Score.ToString() : 0.ToString();
        endlessScore.text = isEndless ? highScores.Find((e) => e.Game == gameMode && e.IsEndless == true).Score.ToString() : 0.ToString();
    }

    private void LoadScores()
    {
        PlayerData data = PlayerSaveManager.LoadInfo();
        try
        {
            highScores = data.HighScores();
        }
        catch (Exception)
        {
            Debug.Log("Score File not found");
        }
    }

    private SpellingGames GetGameMode()
    {
        var currentGameMode = gameModeDropdown.value;

        switch (currentGameMode)
        {
            case 0:
                return SpellingGames.FILLINTHEBLANK;
            case 1:
                return SpellingGames.ANAGRAM;
            case 2:
                return SpellingGames.WORDSEARCH;
            default:
                return SpellingGames.FILLINTHEBLANK;
        }
    }

    private bool HasHighScores(SpellingGames game, Difficulties difficulties)
    {
        return highScores.Contains(highScores.Find((e) => e.Game == game && e.Difficulties == difficulties && e.IsEndless == false));
    }
}


using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomiseGame : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown gameMode;
    [SerializeField] private TMP_Dropdown difficulties;
    [SerializeField] private GameMode game;

    public void SetIsEndlessGame(bool isEndless)
    {
        game.IsEndless = isEndless;
    }

    public void SetGameMode()
    {
        var currentGameMode = gameMode.value;

        switch(currentGameMode)
        {
            case 0:
                game.Game = SpellingGames.FILLINTHEBLANK;
                break;
            case 1:
                game.Game = SpellingGames.ANAGRAM;
                break;
            case 2:
                game.Game = SpellingGames.WORDSEARCH;
                break;
            default:
                break;
        }
    }

    public void SetGameDifficulty()
    {
        var difficulty = difficulties.value;

        switch (difficulty)
        {
            case 0:
                game.Difficulties = Difficulties.EASY;
                break;
            case 1:
                game.Difficulties = Difficulties.MEDIUM;
                break;
            case 2:
                game.Difficulties = Difficulties.HARD;
                break;
            default:
                game.Difficulties = Difficulties.EASY;
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomiseGame : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown gameMode;
    [SerializeField] private TMP_Dropdown difficulties;
    [SerializeField] private TextMeshProUGUI instruction;
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

    public void SetInstruction()
    {
        var currentGameMode = gameMode.value;

        switch (currentGameMode)
        {
            case 0:
                instruction.text = "You will receive a missing character in a given word. Your task is to type the word as your answer.";
                break;
            case 1:
                instruction.text = "A given word will have multiple characters swapping its position. Your task is to type the word as your answer.";
                break;
            case 2:
                instruction.text = "Searching for words in a given board.";
                break;
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//This class is to save the data during every game session
public class RealTimeSavingManager : MonoBehaviour
{
    [SerializeField] ScoreSystem scoreSystem;
    [SerializeField] PerformanceTracker performanceTracker;
    [SerializeField] HighScores highScores;
    [SerializeField] Player player;

    private static RealTimeSavingManager instance;
    public static RealTimeSavingManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    //Saving data after the game is over
    public void Save(GameMode gameMode)
    {

        //Check if the current game mode, type and difficulties already have high scores before
        var highScore = highScores.GetHighScoreDatas().Find((game) => game.Game == gameMode.Game && game.Difficulties == gameMode.Difficulties && game.IsEndless == GameManager.Instance.IsEndless);

        if (highScore != null)
        {
            highScore.Score = scoreSystem.GetUpdateHighScore();
        }
        else
        {
            highScore = new(gameMode.Game, gameMode.Difficulties, scoreSystem.GetUpdateHighScore(), GameManager.Instance.IsEndless);
            highScores.GetHighScoreDatas().Add(highScore);
        }

        //Saving high scores, correct words, and incorrect words of the player in the game session
        PlayerSaveManager.SaveInfo(highScores.GetHighScoreDatas(), performanceTracker.GetCorrectWords(), performanceTracker.GetIncorrectWords());
    }

    //Saving data between scenes
    public void SaveCurrentState()
    {
        InGameSaveManager.SaveInfo(scoreSystem.GetScore(), player.CurrentHealth, player.Lives);
    }

}
 
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    public void Save(GameMode gameMode)
    {
        var highScore = highScores.GetHighScoreDatas().Find((game) => game.Game == gameMode.Game && game.Difficulties == gameMode.Difficulties);

        if(highScore != null)
        {
            highScore.Score = scoreSystem.GetUpdateHighScore();
        }
        else
        {
            highScore = new(gameMode.Game, gameMode.Difficulties, scoreSystem.GetUpdateHighScore(), GameManager.Instance.IsEndless());
            highScores.GetHighScoreDatas().Add(highScore);
        }

        PlayerSaveManager.SavePlayerInfo(highScores.GetHighScoreDatas(), performanceTracker.GetCorrectWords(), performanceTracker.GetIncorrectWords());
    }

    public void SaveCurrentState()
    {
        InGameSaveManager.SaveInGameInfo(scoreSystem.GetScore(), player.CurrentHealth, player.Lives);
    }

}
 
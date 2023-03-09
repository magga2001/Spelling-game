using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeSavingManager : MonoBehaviour
{
    [SerializeField] ScoreSystem scoreSystem;
    [SerializeField] PerformanceTracker performanceTracker;
    [SerializeField] Player player;

    private static RealTimeSavingManager instance;
    public static RealTimeSavingManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }
    public void Save()
    {
        PlayerSaveManager.SavePlayerInfo(scoreSystem.GetUpdateHighScore(), performanceTracker.GetCorrectWords(), performanceTracker.GetIncorrectWords());
    }

    public void SaveCurrentState()
    {
        InGameSaveManager.SaveInGameInfo(scoreSystem.GetScore(), player.CurrentHealth, player.Lives);
    }
}
 
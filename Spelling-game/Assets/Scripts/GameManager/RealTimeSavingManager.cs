using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeSavingManager : MonoBehaviour
{
    [SerializeField] ScoreSystem scoreSystem;
    [SerializeField] PerformanceTracker performanceTracker;
    public void Save()
    {
        PlayerSaveManager.SavePlayerInfo(scoreSystem.GetHighScore(), performanceTracker.GetCorrectWords(), performanceTracker.GetIncorrectWords());
    }
}
 
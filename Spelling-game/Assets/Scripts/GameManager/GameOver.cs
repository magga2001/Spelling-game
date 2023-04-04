using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] ScoreSystem scoreSystem;
    [SerializeField] PerformanceTracker performanceTracker;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI correctWordsText;
    [SerializeField] private TextMeshProUGUI incorrectWordsText;

    private void OnEnable()
    {
        scoreText.text = scoreSystem.GetScore().ToString();
        highScoreText.text = scoreSystem.GetUpdateHighScore().ToString();  
        correctWordsText.text = performanceTracker.GetCurrentSessionCorrectWords().Count.ToString();   
        incorrectWordsText.text = performanceTracker.GetCurrentSessionIncorrectWords().Count.ToString(); 
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneName.sceneOne);
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneName.mainmenu);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Winning : MonoBehaviour
{
    [SerializeField] ScoreSystem scoreSystem;
    [SerializeField] PerformanceTracker performanceTracker;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI correctWordsText;
    [SerializeField] private TextMeshProUGUI incorrectWordsText;

    private void OnEnable()
    {
        //AudioManager.instance.Play("Gameover");

        //scoreText.text = GameManager.score.ToString();

        // highScoreText.text = GameManager.highScore.ToString();

        scoreText.text = scoreSystem.GetScore().ToString();
        highScoreText.text = scoreSystem.GetUpdateHighScore().ToString();
        correctWordsText.text = performanceTracker.GetCorrectWords().Count.ToString();
        incorrectWordsText.text = performanceTracker.GetIncorrectWords().Count.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneName.mainmenu);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Winning : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI correctWordsText;
    [SerializeField] private TextMeshProUGUI incorrectWordsText;

    private void OnEnable()
    {
        //AudioManager.instance.Play("Gameover");

        //scoreText.text = GameManager.score.ToString();

        // highScoreText.text = GameManager.highScore.ToString();

        scoreText.text = 0.ToString();
        highScoreText.text = 0.ToString();
        correctWordsText.text = 0.ToString();
        incorrectWordsText.text = 0.ToString();
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
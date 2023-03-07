using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private RealTimeSavingManager realTimeSavingManager;

    private bool gameIsOver;
    private bool isLevelComplete;

    public bool GameIsOver { get { return gameIsOver; } set { gameIsOver = value; } }

    [SerializeField] VocabularyLibrary library;
    [SerializeField] VocabularyManager vocabularyManager;
    [SerializeField] PuzzlesManager puzzlesManager; 
    [SerializeField] SpellingDifficultiesManager spellingDifficultiesManager;
    [SerializeField] ScoreSystem scoreSystem;
    [SerializeField] StrikeSystem strikeSystem;
    [SerializeField] PerformanceTracker performanceTracker;

    private void Awake()
    {
        instance = this;
        SetUpVocabularies();
        SetUpData();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameIsOver = false;
        isLevelComplete = false;  
    }

    // Update is called once per frame
    void Update()
    {
        if(isLevelComplete)
        {
            WinGame();
        }

        if(gameIsOver)
        {
            EndGame();
        }
    }

    private void SetUpVocabularies()
    {
        library.SetUp();
        spellingDifficultiesManager.SetUp();
        vocabularyManager.SetUp();
        puzzlesManager.SetUp();
    }

    private void SetUpData()
    {
        scoreSystem.SetUp();
        strikeSystem.SetUp();
        performanceTracker.SetUp();
    }

    private void WinGame()
    {
        winUI.SetActive(true);

        realTimeSavingManager.Save();
    }

    private void EndGame()
    {
        gameOverUI.SetActive(true);

        realTimeSavingManager.Save();
    }
}

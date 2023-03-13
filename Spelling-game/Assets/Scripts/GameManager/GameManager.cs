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
    public bool IsLevelComplete { get { return isLevelComplete; } set { isLevelComplete = value; } }

    [SerializeField] VocabularyLibrary library;
    [SerializeField] VocabularyManager vocabularyManager;
    [SerializeField] PuzzlesManager puzzlesManager;
    [SerializeField] GameMode gameMode;
    [SerializeField] SpellingGameManager spellingGameManager;   
    [SerializeField] SpellingDifficultiesManager spellingDifficultiesManager;
    [SerializeField] ScoreSystem scoreSystem;
    [SerializeField] StrikeSystem strikeSystem;
    [SerializeField] PerformanceTracker performanceTracker;

    [SerializeField] private bool isFirstScene;
    private bool isEndless;
    public bool IsEndless() => isEndless;

    private void Awake()
    {
        instance = this;

        isEndless = gameMode.IsEndless;

        if(isFirstScene)
        {
            InGameSaveManager.DeleteProgess();
            SetUpVocabularies();
            SetUpData();
        }
        else
        {
            Debug.Log("Not first scene");
        }

        spellingGameManager.SetUp(gameMode.Game);
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

        if (isLevelComplete)
        {
            WinGame();
        }

        if(gameIsOver)
        {
            EndGame();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            PlayerSaveManager.DeleteProgess();
        }
    }

    private void SetUpVocabularies()
    {
        library.SetUp();
        spellingDifficultiesManager.SetUp(gameMode.Difficulties);
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

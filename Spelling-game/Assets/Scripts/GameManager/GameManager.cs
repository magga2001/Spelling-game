using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private bool gameIsOver;

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
    }

    // Update is called once per frame
    void Update()
    {

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
}

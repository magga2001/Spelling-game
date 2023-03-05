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
    [SerializeField] SpellingDifficultiesManager spellingDifficultiesManager;

    private void Awake()
    {
        instance = this;
        library.SetUp();
        spellingDifficultiesManager.SetUp();
        vocabularyManager.SetUp();
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
}

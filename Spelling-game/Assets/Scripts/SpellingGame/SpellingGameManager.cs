using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class SpellingGameManager : MonoBehaviour
{
    [System.Serializable]
    public class SpellingGame
    {
        [SerializeField] private string name;
        [SerializeField] private GameObject canvas;
        [SerializeField] private GameObject game;

        public string Name { get { return name; } set { name = value; } }
        public GameObject Canvas { get { return canvas; } set { canvas = value; } }
        public GameObject Game { get { return game; } set { game = value; } }
    }

    [SerializeField] private SpellingGame[] games;

    private int currentGame = 0;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            GenerateRandomSpellingGame();
        }
    }
    public void GenerateRandomSpellingGame()
    {
        games[currentGame].Canvas.SetActive(false);
        games[currentGame].Game.SetActive(false);

        Random rnd = new();
        int number = rnd.Next(0, games.Length);
        currentGame = number;

        games[currentGame].Canvas.SetActive(true);
        games[currentGame].Game.SetActive(true);

        //Debug.Log(games[currentGame].Name);

    }

    public void ResetScreen()
    {
        games[currentGame].Canvas.SetActive(false);
        games[currentGame].Game.SetActive(false);
    }
}

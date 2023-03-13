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
        [SerializeField] private SpellingGames gameName;
        [SerializeField] private GameObject canvas;
        [SerializeField] private GameObject game;

        public string Name { get { return name; } set { name = value; } }

        public SpellingGames GameName { get { return gameName; } set { gameName = value; } }
        public GameObject Canvas { get { return canvas; } set { canvas = value; } }
        public GameObject Game { get { return game; } set { game = value; } }
    }

    [SerializeField] private List<SpellingGame> games;

    private SpellingGame currentSpellingGame;

    public void SetUp(SpellingGames game)
    {
        currentSpellingGame = games.Find((e) => e.GameName == game);
    }
    private void Update()
    {

    }
    public void DisplaySpellingGame()
    {

        currentSpellingGame.Canvas.SetActive(true);
        currentSpellingGame.Game.SetActive(true);
    }

    public void ResetScreen()
    {
        currentSpellingGame.Canvas.SetActive(false);
        currentSpellingGame.Game.SetActive(false);
    }
}

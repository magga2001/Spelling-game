using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum Difficulties { EASY, MEDIUM, HARD };

    private int XP_per_difficulties;

    private bool GameIsOver;
    private bool spelling_mode;

    [SerializeField] private Player player;

    // Start is called before the first frame update
    void Start()
    {
        GameIsOver = false;
        // Change this later when saving the current game mode
        spelling_mode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Lives <= 0)
        {
            GameIsOver = true;
        }
    }
}

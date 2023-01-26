using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool GameIsOver;

    [SerializeField] private Player player;

    // Start is called before the first frame update
    void Start()
    {
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Life <= 0)
        {
            GameIsOver = true;
        }
    }
}

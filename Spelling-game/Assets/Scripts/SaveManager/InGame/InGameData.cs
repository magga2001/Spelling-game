using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InGameData
{
    [SerializeField] private int score;
    [SerializeField] private int health;
    [SerializeField] private int lives;
    public InGameData(int score, int health, int lives )
    {
        this.score = score;
        this.health = health;
        this.lives = lives;
    }

    public int Score() => score;
    public int Health() => health;  
    public int Lives() => lives;    
}

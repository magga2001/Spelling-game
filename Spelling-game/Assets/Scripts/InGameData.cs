using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InGameData
{
    public int score;
    public int health;
    public int lives;
    public InGameData(int score, int health, int lives )
    {
        this.score = score;
        this.health = health;
        this.lives = lives;
    }
}

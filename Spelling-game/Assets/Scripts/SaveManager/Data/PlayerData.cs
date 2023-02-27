using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int highScore;
    public IDictionary<string, int> numberNames = new Dictionary<string, int>();

    public PlayerData(int highScore, IDictionary<string, int> numberNames)
    {
        this.highScore = highScore;
        this.numberNames = numberNames; 
    }
}

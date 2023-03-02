using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem : MonoBehaviour
{
    [SerializeField] ScoreSystem scoreSystem;
    public void CalculateReward(string word)
    {
        scoreSystem.IncreaseScore(word.Length);
    }
}

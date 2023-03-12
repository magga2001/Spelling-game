using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunishmentSystem : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private StrikeSystem strikeSystem;

    public void CalculatePunishment(string word)
    {
        if(GameManager.Instance.IsEndless())
        {
            strikeSystem.IncreaseStrike();
        }
        scoreSystem.DecreaseScore(word.Length);
    }
}

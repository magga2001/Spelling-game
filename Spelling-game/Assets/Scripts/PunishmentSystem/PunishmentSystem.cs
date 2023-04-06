using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunishmentSystem : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private StrikeSystem strikeSystem;

    public void CalculatePunishment(string word)
    {
        //Strike system only triggered if game is endless
        if(GameManager.Instance.IsEndless)
        {
            strikeSystem.IncreaseStrike();
        }
        scoreSystem.DecreaseScore(word.Length);
    }
}

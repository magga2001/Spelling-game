using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunishmentSystem : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;   
    [SerializeField] private int maxStrike;

    private int strike;

    public void CalculatePunishment(string word)
    {
        IncreaseStrike();
        scoreSystem.DecreaseScore(word.Length);
    }

    private void IncreaseStrike()
    {
        strike++;
        if (strike >= maxStrike)
        {
            strike = 0;
            //DO something
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StrikeSystem : ScriptableObject
{
    [SerializeField] private int maxStrike;
    [SerializeField] private VocabularyManager vocabularyManager;
    [SerializeField] private SpellingDifficultiesManager spellingDifficultiesManager;
    private int strike;

    public void SetUp()
    {
        strike = 0;
    }
    public void IncreaseStrike()
    {
        strike++;
        //If strike hits maximum strike, demote difficulty of the game and reset strike
        if (strike >= maxStrike)
        {
            strike = 0;
            spellingDifficultiesManager.DemoteDifficulty();
            vocabularyManager.SetUpDifficulty(spellingDifficultiesManager.Difficulties);

        }
    }
}

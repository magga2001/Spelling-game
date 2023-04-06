using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu]
public class StrikeSystem : ScriptableObject
{
    [SerializeField] private int maxStrike;
    [SerializeField] private VocabularyManager vocabularyManager;
    [SerializeField] private SpellingDifficultiesManager spellingDifficultiesManager;
    private int strike;
    public int Strike { get { return strike; } set { strike = value; } }

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
            var currentDifficulty = spellingDifficultiesManager.Difficulties;
            spellingDifficultiesManager.DemoteDifficulty();
            if(spellingDifficultiesManager.Difficulties != currentDifficulty)
            {
                vocabularyManager.SetUpDifficulty(spellingDifficultiesManager.Difficulties);
            }
        }
    }
}

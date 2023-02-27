using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellingDifficultiesManager : MonoBehaviour
{

    [SerializeField] private int XP_per_difficulties;

    private int currentXP;

    private Difficulties currentDifficulties;
    public Difficulties Difficulties { get { return currentDifficulties; } }    

    private void Awake()
    {
        currentXP = 0;
        currentDifficulties = Difficulties.MEDIUM;
    }

    public void PromoteDifficulty()
    {
        switch (currentDifficulties)
        {
            case Difficulties.EASY:
                currentDifficulties = Difficulties.MEDIUM;
                break;
            case Difficulties.MEDIUM:
                currentDifficulties = Difficulties.HARD;
                break;
        }
    }

    public void DemoteDifficulty()
    {
        switch (currentDifficulties)
        {
            case Difficulties.MEDIUM:
                currentDifficulties = Difficulties.EASY;
                break;
            case Difficulties.HARD:
                currentDifficulties = Difficulties.MEDIUM;
                break;
        }
    }

    public void IncreaseXP(int newXP)
    {
        currentXP += newXP;
        if(currentXP >= XP_per_difficulties)
        {
            currentXP -= XP_per_difficulties;
            PromoteDifficulty();    
        }
    }

    public void DecreaseXP(int newXP)
    {
        currentXP -= newXP;
        if(currentXP <= 0)
        {
            currentXP += XP_per_difficulties;
            DemoteDifficulty();
        }
    }
}

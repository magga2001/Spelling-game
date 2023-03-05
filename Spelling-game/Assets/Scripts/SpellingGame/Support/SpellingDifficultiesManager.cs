using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellingDifficultiesManager : MonoBehaviour
{
    private Difficulties currentDifficulties;
    public Difficulties Difficulties { get { return currentDifficulties; } }    

    private void Awake()
    {
        currentDifficulties = Difficulties.EASY;
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
}

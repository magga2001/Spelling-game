using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class SpellingDifficultiesManager : ScriptableObject
{
    private Difficulties currentDifficulties;
    public Difficulties Difficulties { get { return currentDifficulties; } }    

    public void SetUp(Difficulties difficulties)
    {
        currentDifficulties = difficulties;
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
            default:
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
            default:
                break;
        }
    }
}

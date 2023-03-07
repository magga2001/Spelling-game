using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class PerformanceTracker : ScriptableObject
{
    private List<string> correctWords = new List<string>();
    private List<string> incorrectWords = new List<string>();

    public void SetUp()
    {

        PlayerData data = PlayerSaveManager.LoadPlayerInfo();
        try
        {
            correctWords = data.correctWords;
            incorrectWords = data.IncorrectWords;
        }
        catch (Exception)
        {
            correctWords.Clear();
            incorrectWords.Clear();
            Debug.Log("Performance File not found");
        }
    }

    public void AddCorrectWord(string newWord)
    {
        if (!correctWords.Contains(newWord))
        {
            correctWords.Add(newWord);
            UpdatePerformanceStat();
        }
    }

    public void AddIncorrectWord(string newWord)
    {
        if (!incorrectWords.Contains(newWord))
        {
            incorrectWords.Add(newWord);
            if(correctWords.Contains(newWord))
            {
                correctWords.Remove(newWord);
            }
        }
    }

    public List<string> GetCorrectWords()
    {
        return correctWords.Distinct().ToList();  
    }

    public List<string> GetIncorrectWords()
    {
        return incorrectWords.Distinct().ToList();
    }

    public void UpdatePerformanceStat()
    {
        incorrectWords = incorrectWords.FindAll(word => correctWords.Contains(word));   
    }
}

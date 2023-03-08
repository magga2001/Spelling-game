using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class PerformanceTracker : ScriptableObject
{
    private List<string> currentSessionCorrectWords = new List<string>();
    private List<string> currentSessionIncorrectWords = new List<string>();

    [HideInInspector] [SerializeField] private List<string> correctWords = new List<string>();
    [HideInInspector] [SerializeField] private List<string> incorrectWords = new List<string>();

    public void SetUp()
    {
        currentSessionCorrectWords.Clear();
        currentSessionIncorrectWords.Clear();

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

    public void AddCurrentSessionCorrectWord(string newWord)
    {
        if (!currentSessionCorrectWords.Contains(newWord) && !currentSessionIncorrectWords.Contains(newWord))
        {
            currentSessionCorrectWords.Add(newWord);
        }
    }
    public void AddCurrentSessionIncorrectWord(string newWord)
    {
        if (!currentSessionIncorrectWords.Contains(newWord))
        {
            currentSessionIncorrectWords.Add(newWord);
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

    public List<string> GetCurrentSessionCorrectWords()
    {
        return currentSessionCorrectWords;
    }

    public List<string> GetCurrentSessionIncorrectWords()
    {
        return currentSessionIncorrectWords;
    }


    public List<string> GetCorrectWords()
    {
        UpdatePerformanceStat();
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

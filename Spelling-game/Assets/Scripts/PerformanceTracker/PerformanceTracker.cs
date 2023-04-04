using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class PerformanceTracker : ScriptableObject
{
    //List of correct words in a game session
    private List<string> currentSessionCorrectWords = new List<string>();
    //List of incorrect words in a game session
    private List<string> currentSessionIncorrectWords = new List<string>();

    //List of correct words permanently
    [HideInInspector] [SerializeField] private List<string> correctWords;
    //List of correct words permanently
    [HideInInspector] [SerializeField] private List<string> incorrectWords;

    //This method is called everytime enter the game session
    public void SetUp()
    {
        //Reset for the new game session
        currentSessionCorrectWords.Clear();
        currentSessionIncorrectWords.Clear();

        PlayerData data = PlayerSaveManager.LoadInfo();
        try
        {
            correctWords = data.CorrectWords();
            incorrectWords = data.IncorrectWords();
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

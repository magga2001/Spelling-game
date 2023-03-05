using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceTracker : MonoBehaviour
{
    private List<string> correctWords = new List<string>();
    private List<string> incorrectWords = new List<string>();

    private void Awake()
    {

        PlayerData data = PlayerSaveManager.LoadPlayerInfo();
        try
        {
            correctWords = data.correctWords;
            incorrectWords = data.IncorrectWords;
        }
        catch (Exception)
        {
            Debug.Log("File not found");
        }
    }

    public void AddCorrectWord(string newWord)
    {
        correctWords.Add(newWord);  
    }

    public void AddIncorrectWord(string newWord)
    {
        incorrectWords.Add(newWord);
    }

    public List<string> GetCorrectWords()
    {
        return correctWords;  
    }

    public List<string> GetIncorrectWords()
    {
        return incorrectWords;
    }

    public void UpdatePerformanceStat()
    {
        incorrectWords = incorrectWords.FindAll(word => correctWords.Contains(word));   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceTracker : MonoBehaviour
{
    private List<string> correctWords = new List<string>();
    private List<string> incorrectWords = new List<string>();
    
    public void AddCorrectWord(string newWord)
    {
        correctWords.Add(newWord);  
    }

    public void AddIncorrectWord(string newWord)
    {
        incorrectWords.Add(newWord);
    }

    public int NumberOfCorrectWord()
    {
        return correctWords.Count;  
    }

    public int NumberOfIncorrectWord()
    {
        return incorrectWords.Count;
    }
}

  using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu]
public class VocabularyManager : ScriptableObject
{
    [SerializeField] private VocabularyLibrary library;
    [SerializeField] private SpellingDifficultiesManager spellingDifficultiesManager;
    private Queue<Vocabulary> vocabularies = new ();
    private Vocabulary currentVocabulary;
    private string currentWord;
    private string currentDefinition;

    //Set up the difficulty at the start of the game session
    public void SetUp()
    {
        SetUpDifficulty(spellingDifficultiesManager.Difficulties);
    }

    //Load up all the words from vocabulary library and add to the queue for the game session
    public void SetUpDifficulty(Difficulties difficulties)
    {
        vocabularies.Clear();
         
        var vocabulariesLibrary = new List<Vocabulary>();  

        switch (difficulties)
        {
            case Difficulties.EASY:
                vocabulariesLibrary = library.GetEasyVocabularies().Vocabularies;
                break;
            case Difficulties.MEDIUM:
                vocabulariesLibrary = library.GetMediumVocabularies().Vocabularies;
                break;
            case Difficulties.HARD:
                vocabulariesLibrary = library.GetHardVocabularies().Vocabularies;
                break;
        }

        Randomize(vocabulariesLibrary);

        foreach(var vocabulary in vocabulariesLibrary)
        {
            vocabularies.Enqueue(vocabulary);
        }

        DequeueWord();
    }

    public void NextWord()
    {
        if(vocabularies.Count != 0)
        {
            DequeueWord();
        }
        else
        {
            //If the game is endless, then go to next difficulty
            //Otherwise stay the same difficulty
            if (GameManager.Instance.IsEndless())
            {
                spellingDifficultiesManager.PromoteDifficulty();
            }
            SetUpDifficulty(spellingDifficultiesManager.Difficulties);
        }
    }

    private void DequeueWord()
    {
        var vocab = vocabularies.Dequeue();

        currentVocabulary = vocab;
        currentWord = vocab.Word;
        currentDefinition = vocab.Definition;
    }

    //If spelled wrong, put the incorrectly spelt word to the back of the queue.
    public void Requeue()
    {
        if (!vocabularies.Contains(currentVocabulary))
        {
            vocabularies.Enqueue(currentVocabulary);
        }
    }

    public string GetCurrentWord()
    {
        return currentWord;
    }

    public string GetCurrentWordDefinition()
    {
        return currentDefinition;
    }

    public bool IsEmptyVocabularies()
    {
        return vocabularies.Count == 0;
    }

    //Shuffle the list for each element to be in random position than the original
    private void Randomize(List<Vocabulary> inputList)
    {
        int listSize = inputList.Count;
        while (listSize > 1)
        {
            listSize--;
            Random randomGenerator = new();
            int randomIndex = randomGenerator.Next(listSize + 1);
            (inputList[listSize], inputList[randomIndex]) = (inputList[randomIndex], inputList[listSize]);
        }
    }
}

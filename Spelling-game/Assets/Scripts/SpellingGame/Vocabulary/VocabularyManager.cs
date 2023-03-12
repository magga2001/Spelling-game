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

    public void SetUp()
    {
        SetUpDifficulty(spellingDifficultiesManager.Difficulties);
    }

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

        Shuffle(vocabulariesLibrary);

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

            //If hard is now cleared, bring up the incorrect word.
            //If spelled wrong, put it at the back of the queue.

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

    private void Shuffle(List<Vocabulary> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            Random rnd = new();
            int i = rnd.Next(n + 1);
            (list[n], list[i]) = (list[i], list[n]);
        }
    }
}

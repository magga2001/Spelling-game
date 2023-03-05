  using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocabularyManager : MonoBehaviour
{
    [SerializeField] private VocabularyLibrary library;
    private SpellingDifficultiesManager spellingDifficultiesManager;
    private Queue<Vocabulary> vocabularies = new ();
    private string currentWord;
    private string currentDefinition;

    // Start is called before the first frame update
    void Start()
    {
        SetUpDifficulty(Difficulties.MEDIUM);
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
            spellingDifficultiesManager.PromoteDifficulty();
            SetUpDifficulty(spellingDifficultiesManager.Difficulties);
        }
    }

    private void DequeueWord()
    {
        var word = vocabularies.Dequeue();

        currentWord = word.Word;
        currentDefinition = word.Definition;

        //Debug.Log(currentWord);
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
}

using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Vocabulary
{
    [SerializeField] private string word;
    [SerializeField] private string definition;
    public string Word { get { return word; } set { word = value; } }
    public string Definition { get { return definition; } set { definition = value; } }
}

[Serializable]
public class VocabularyList
{
    [SerializeField] private string difficulty;
    [SerializeField] private List<Vocabulary> vocabularies = new List<Vocabulary>();
    public string Difficulty { get { return difficulty; } set { difficulty = value; } }
    public List<Vocabulary> Vocabularies { get { return vocabularies; } set { vocabularies = value; } }
}

public class VocabularyLibrary : MonoBehaviour
{
    [SerializeField] private List<TextAsset> vocabulariesJson;
    private List<VocabularyList> vocabularyCategories = new List<VocabularyList>();
    void Awake()
    {
        foreach (var vocabularyJson in vocabulariesJson)
        {
            if (!string.IsNullOrEmpty(vocabularyJson.text))
            {
                vocabularyCategories.Add(JsonUtility.FromJson<VocabularyList>(vocabularyJson.text));
            }
        }


        //For debugging purpose
        //foreach (var vocabularyCategory in vocabularyCategories)
        //{
            //Debug.Log("Difficulty: " + vocabularyCategory.Difficulty);
            //if (vocabularyCategory.Vocabularies.Count > 0)
            //{
                //Debug.Log(vocabularyCategory.Vocabularies[0].Word);
                //Debug.Log(vocabularyCategory.Vocabularies[0].Definition);
            //}
            //else
            //{
                //Debug.LogWarning("No vocabularies found for difficulty " + vocabularyCategory.Difficulty);
            //}
        //}
    }

    public VocabularyList GetEasyVocabularies()
    {
        return vocabularyCategories.Find(e => e.Difficulty == "easy");
    }

    public VocabularyList GetMediumVocabularies()
    {
        return vocabularyCategories.Find(e => e.Difficulty == "medium");
    }

    public VocabularyList GetHardVocabularies()
    {
        return vocabularyCategories.Find(e => e.Difficulty == "hard");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocabularyManager : MonoBehaviour
{
    [System.Serializable]
    public class VocabularyLevel
    {
        [SerializeField] private int level;
        [SerializeField] private string[] words;

        public int Level { get { return level; } set { level = value; } }
        public string[] Words { get { return words; } set { words = value; } }
    }

    [SerializeField] private VocabularyLevel[] vocabularyLevels;
    private int currentWordIndex;
    private int currentLevel;
    private string currentWord;

    //public string CurrentWord { get { return currentWord; } set { currentWord = value; } }

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 1;
        currentWordIndex = 0;
        currentWord = vocabularyLevels[currentLevel - 1].Words[currentWordIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextWord()
    {
        currentWordIndex++;
        if (currentWordIndex < vocabularyLevels[currentLevel - 1].Words.Length)
        {
            currentWord = vocabularyLevels[currentLevel - 1].Words[currentWordIndex];
        }
        else
        {
            //For now reset to first word
            currentWordIndex = 0;
            currentWord = vocabularyLevels[currentLevel - 1].Words[currentWordIndex];
        }
    }

    public string GetCurrentWord()
    {
        return vocabularyLevels[currentLevel - 1].Words[currentWordIndex];
    }
}

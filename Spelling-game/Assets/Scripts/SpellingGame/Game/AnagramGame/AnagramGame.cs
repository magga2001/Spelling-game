using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Random = System.Random;
using Unity.VisualScripting;

public class AnagramGame : Subject<(PlayerAction, PlayerAnswerData)>
{
    [SerializeField] private VocabularyManager vm;
    [SerializeField] private TextMeshProUGUI word;
    [SerializeField] private TextMeshProUGUI definition;
    [SerializeField] private TMP_InputField inputText;

    private string currentAnswer;

    private void OnEnable()
    {
        if (!vm.IsEmptyVocabularies())
        {
            CreateAnagram();
        }
        inputText.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer();
        }

        if (GameManager.Instance.GameIsOver)
        {
            gameObject.SetActive(false);
        }
    }

    public void UpdateAnswer(string input)
    {
        currentAnswer = input;

    }

    public void CheckAnswer()
    {
        string answer = vm.GetCurrentWord().Trim();
        currentAnswer = currentAnswer.Trim().ToLower();

        if (answer == currentAnswer)
        {
            vm.NextWord();
            NotifyObservers((PlayerAction.SPELLED_CORRECT, new(SpellingGames.ANAGRAM, answer, currentAnswer)));
            inputText.text = "";
            CreateAnagram();
        }
        else
        {
            vm.Requeue();
            NotifyObservers((PlayerAction.SPELLED_WRONG, new(SpellingGames.ANAGRAM, answer, currentAnswer)));
        }
        inputText.ActivateInputField();
    }

    private void CreateAnagram()
    {
        // Get the current word and definition from the vocabulary manager
        string correctWord = vm.GetCurrentWord();
        string wordDefinition = vm.GetCurrentWordDefinition();

        //Shuffle and swap position randomly in the current word string
        var anagramWord = RandomizeCharacters(correctWord.ToList());

        //Ensure that the swap does not result in the original position
        while (correctWord.Equals(anagramWord))
        {
            anagramWord = RandomizeCharacters(correctWord.ToList());
        }

        //Display on the game's screen
        word.text = anagramWord;
        definition.text = wordDefinition;
    }

    //Shuffle the position between characters
    private string RandomizeCharacters(List<char> inputList)
    {
        Random randomGenerator = new Random();
        int randomLimit = randomGenerator.Next(2, inputList.Count);
        while (randomLimit > 1)
        {
            randomLimit--;
            Random newRandom = new();
            int randomIndex = newRandom.Next(randomLimit + 1);
            (inputList[randomLimit], inputList[randomIndex]) = (inputList[randomIndex], inputList[randomLimit]);
        }

        string shuffledString = new string(inputList.ToArray());

        return shuffledString;
    }

}
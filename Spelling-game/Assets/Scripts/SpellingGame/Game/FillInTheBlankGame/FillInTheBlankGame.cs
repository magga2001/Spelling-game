using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillInTheBlankGame : Subject<(PlayerAction, PlayerAnswerData)>
{
    [SerializeField] private VocabularyManager vm;
    [SerializeField] private TextMeshProUGUI word;
    [SerializeField] private TextMeshProUGUI definition;
    [SerializeField] private TMP_InputField inputText;
    private string currentAnswer;

    //Set up the first Fill-in-the-blank question
    private void OnEnable()
    {
        if(!vm.IsEmptyVocabularies())
        {
            CreateBlankCharacter();
        }
        inputText.ActivateInputField();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer();
        }

        if(GameManager.Instance.GameIsOver)
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

        //Check the answer if the player spells the word correctly
        if (answer == currentAnswer.Trim())
        {
            vm.NextWord();
            NotifyObservers((PlayerAction.SPELLED_CORRECT, new(SpellingGames.FILLINTHEBLANK, answer, currentAnswer)));
            inputText.text = "";
            CreateBlankCharacter();
        }
        else
        {
            vm.Requeue();
            NotifyObservers((PlayerAction.SPELLED_WRONG, new(SpellingGames.FILLINTHEBLANK, answer, currentAnswer)));
        }

        inputText.ActivateInputField();
    }

    private void CreateBlankCharacter()
    {
        // Get the current word and definition from the vocabulary manager
        string correctWord = vm.GetCurrentWord();
        string wordDefinition = vm.GetCurrentWordDefinition();

        // Randomise the index of the word which is a string
        Random rnd = new();
        int number = rnd.Next(0, correctWord.Length);

        //Replace "_" for generated random index  
        word.text = correctWord.Substring(0, number) + "_" + correctWord.Substring(number + 1);
        definition.text = wordDefinition;
    }
}

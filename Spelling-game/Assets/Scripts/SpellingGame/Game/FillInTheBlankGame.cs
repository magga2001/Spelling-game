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
        string correctWord = vm.GetCurrentWord();
        string wordDefinition = vm.GetCurrentWordDefinition();

        Random rnd = new();
        int number = rnd.Next(0, correctWord.Length);

        word.text = correctWord.Substring(0, number) + "_" + correctWord.Substring(number + 1);
        definition.text = wordDefinition;
    }
}

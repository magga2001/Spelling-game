using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSpeechGame : Subject<(PlayerAction, PlayerAnswerData)>
{
    [SerializeField] private VocabularyManager vm;
    [SerializeField] private TMP_InputField inputText;
    private string currentAnswer;

    private void OnEnable()
    {
        if (!vm.IsEmptyVocabularies())
        {
            //Run the text speech
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer();
        }
    }

    public void UpdateAnswer(string input)
    {
        currentAnswer = input;

    }

    public void CheckAnswer()
    {
        string answer = vm.GetCurrentWord().Trim();
        currentAnswer = currentAnswer.Trim();

        if (answer == currentAnswer)
        {
            vm.NextWord();
            NotifyObservers((PlayerAction.SPELLED_CORRECT, new(SpellingGames.TEXTSPEECH, answer, currentAnswer)));
            inputText.text = "";
            //Call the text speech method
        }
        else
        {
            vm.Requeue();
            NotifyObservers((PlayerAction.SPELLED_WRONG, new(SpellingGames.TEXTSPEECH, answer, currentAnswer)));
        }
    }
}
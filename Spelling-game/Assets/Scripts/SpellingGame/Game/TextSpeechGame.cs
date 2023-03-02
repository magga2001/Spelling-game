using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSpeechGame : Subject<(PlayerAction, PlayerAnswerData)>
{
    [SerializeField] private VocabularyManager vm;
    [SerializeField] private int round;
    [SerializeField] private TMP_InputField inputText;
    private string currentAnswer;

    // Start is called before the first frame update
    void Start()
    {

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
        }
        else
        {
            NotifyObservers((PlayerAction.SPELLED_WRONG, new(SpellingGames.TEXTSPEECH, answer, currentAnswer)));
        }
    }
}
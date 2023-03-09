using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Random = System.Random;

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
            CreateAnagram();
        }
        else
        {
            vm.Requeue();
            NotifyObservers((PlayerAction.SPELLED_WRONG, new(SpellingGames.TEXTSPEECH, answer, currentAnswer)));
        }
        inputText.ActivateInputField();
    }

    private void CreateAnagram()
    {
        string correctWord = vm.GetCurrentWord();
        string wordDefinition = vm.GetCurrentWordDefinition();

        word.text = Shuffle(correctWord.ToList());
        definition.text = wordDefinition;


    }

    private string Shuffle(List<char> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            Random rnd = new();
            int i = rnd.Next(n + 1);
            (list[n], list[i]) = (list[i], list[n]);
        }

        var anagram = new string(list.ToArray());

        return anagram;
    }
}
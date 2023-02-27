using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSpeechGame : MonoBehaviour
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
        string answer = vm.GetCurrentWord();

        if (answer == currentAnswer)
        {
            Debug.Log("Correct");
            vm.NextWord();
            inputText.text = "";
            //For now static, but XP SHOULD INCREASE BY WORD DIFFICULTY... maybe word length
            ScoreSystem.Instance.IncreaseScore(CalculateReward(answer));
        }
        else
        {
            Debug.Log("Incorrect");
        }
    }

    private int CalculateReward(string word)
    {
        return word.Length * 100;
    }
}
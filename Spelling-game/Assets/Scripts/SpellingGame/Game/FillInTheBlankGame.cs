using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillInTheBlankGame : MonoBehaviour
{
    [SerializeField] private VocabularyManager vm;
    [SerializeField] private int round;
    [SerializeField] private TextMeshProUGUI word;
    [SerializeField] private TMP_InputField inputText;
    private string currentAnswer;

    // Update is called once per frame
    void Update()
    {
        //Temporary
        if (Input.GetKey(KeyCode.T))
        {
            CreateBlankCharacter();
        }

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

        if (answer == currentAnswer.Trim())
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

    private void CreateBlankCharacter()
    {
        string correctWord = vm.GetCurrentWord();

        Random rnd = new Random();
        int number = rnd.Next(0, correctWord.Length);

        word.text = correctWord.Substring(0, number) + "_" + correctWord.Substring(number + 1);
    }

    private int CalculateReward(string word)
    {
        return word.Length * 100;
    }


}

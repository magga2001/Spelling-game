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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Temporary
        if (Input.GetKey(KeyCode.T))
        {
            createBlankCharacter();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            checkAnswer();
        }
    }

    public void updateAnswer(string input)
    {
        currentAnswer = input;
    }

    public void checkAnswer()
    {
        string answer = vm.GetCurrentWord();

        if (answer == currentAnswer)
        {
            Debug.Log("Correct");
            vm.NextWord();
            inputText.text = "";
            //For now static, but XP SHOULD INCREASE BY WORD DIFFICULTY... maybe word length
            CombatSystem.Instance.IncreaseSessionXP(10);
        }
        else
        {
            Debug.Log("Incorrect");
        }
    }

    private void createBlankCharacter()
    {
        string correctWord = vm.GetCurrentWord();

        Random rnd = new Random();
        int number = rnd.Next(0, correctWord.Length);

        word.text = correctWord.Substring(0, number) + "_" + correctWord.Substring(number + 1);
    }
}

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
            checkAnswer();
        }
    }

    public void updateAnswer(string input)
    {
        currentAnswer = input;

    }

    public void checkAnswer()
    {
        string answer = vm.getCurrentWord();

        if (answer == currentAnswer)
        {
            Debug.Log("Correct");
            vm.nextWord();
            inputText.text = "";
        }
        else
        {
            Debug.Log("Incorrect");
        }
    }
}
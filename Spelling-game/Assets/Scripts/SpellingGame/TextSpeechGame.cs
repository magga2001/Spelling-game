using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpeechGame : MonoBehaviour
{
    [SerializeField] private VocabularyManager vm;
    [SerializeField] private int round;
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
        Debug.Log(input);

    }

    public void checkAnswer()
    {
        string answer = vm.getCurrentWord();

        if (answer == currentAnswer)
        {
            Debug.Log("Correct");
            vm.nextWord();
        }
        else
        {
            Debug.Log("Incorrect");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;

public class FillInTheBlankGame : MonoBehaviour
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
        if (Input.GetKey(KeyCode.Return))
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
        }else
        {
            Debug.Log("Incorrect");
        }
    }

    private void createBlankCharacter()
    {
        string correctWord = vm.getCurrentWord();

        Random rnd = new Random();
        int number = rnd.Next(0, correctWord.Length);

    }
}

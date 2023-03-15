using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private PerformanceTracker performanceTracker;

    [SerializeField] private TextMeshProUGUI correctWordsText;
    [SerializeField] private TextMeshProUGUI incorrectWordsText;

    [SerializeField] private Button correctWordsLeftButton;
    [SerializeField] private Button correctWordsRightButton;
    [SerializeField] private Button incorrectWordsLeftButton;
    [SerializeField] private Button incorrectWordsRightButton;

    [SerializeField] private int maxWordsPerPage;

    private int correctWordsPage = 0;
    private int incorrectWordsPage = 0;

    private List<string> correctWords = new List<string>();
    private List<string> incorrectWords = new List<string>();

    private void Start()
    {
        //correctWords = performanceTracker.GetCorrectWords();
        //incorrectWords = performanceTracker.GetIncorrectWords();

        PlayerData data = PlayerSaveManager.LoadPlayerInfo();

        try
        {
            correctWords = data.CorrectWords;
            incorrectWords = data.IncorrectWords;
        }
        catch
        {
            correctWords = performanceTracker.GetCorrectWords();
            incorrectWords = performanceTracker.GetIncorrectWords();
        }

        if (correctWords.Count == 0)
        {
            correctWordsLeftButton.interactable = false;
            correctWordsRightButton.interactable = false;
        }

        if (incorrectWords.Count == 0)
        {
            incorrectWordsLeftButton.interactable = false;
            incorrectWordsRightButton.interactable = false;
        }

        GetCorrectWords();
        GetIncorrectWords();
    }

    public void CorrectWordsLeftButton()
    {
        correctWordsPage--;
        if (correctWordsPage <= 0)
        {
            correctWordsPage = 0;
        }
        GetCorrectWords();
    }

    public void CorrectWordsRightButton()
    {
        correctWordsPage++;
        if (correctWordsPage >= Mathf.Ceil(correctWords.Count / (float)maxWordsPerPage) - 1)
        {
            correctWordsPage = Mathf.CeilToInt(correctWords.Count / (float)maxWordsPerPage) - 1;
        }
        GetCorrectWords();
    }

    public void IncorrectWordsLeftButton()
    {
        incorrectWordsPage--;
        if (incorrectWordsPage <= 0)
        {
            incorrectWordsPage = 0;
        }
        GetIncorrectWords();
    }

    public void IncorrectWordsRightButton()
    {
        incorrectWordsPage++;
        if (incorrectWordsPage >= Mathf.Ceil(performanceTracker.GetIncorrectWords().Count / (float)maxWordsPerPage) - 1)
        {
            incorrectWordsPage = Mathf.CeilToInt(incorrectWords.Count / (float)maxWordsPerPage) - 1;
        }
        GetIncorrectWords();
    }

    private void GetCorrectWords()
    {
        correctWordsText.text = "";
        int startIndex = correctWordsPage * maxWordsPerPage;
        int endIndex = Mathf.Min(correctWords.Count, startIndex + maxWordsPerPage);

        for (int i = startIndex; i < endIndex; i++)
        {
            correctWordsText.text += "- " + correctWords[i] + "\n";
        }
    }

    private void GetIncorrectWords()
    {
        incorrectWordsText.text = "";
        int startIndex = incorrectWordsPage * maxWordsPerPage;
        int endIndex = Mathf.Min(incorrectWords.Count, startIndex + maxWordsPerPage);

        for (int i = startIndex; i < endIndex; i++)
        {
            incorrectWordsText.text += "- " + incorrectWords[i] + "\n";
        }
    }
}



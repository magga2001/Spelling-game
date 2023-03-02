using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour, IObserver<NotificationText>
{
    [SerializeField] private List<Subject<NotificationText>> spellingGames;
    [SerializeField] private GameObject VocabularyResultNoti;
    [SerializeField] private TextMeshProUGUI VocabularyResultText;

    [SerializeField] private float disableNotiTime;

    private void Start()
    {
        spellingGames.ForEach((game) => game.AddObserver(this));
    }

    public void OnNotify(NotificationText text)
    {
        DisplayVocabularyResult(text);
    }

    public void DisplayVocabularyResult(NotificationText text)
    {
        if(text == NotificationText.CORRECT)
        {
            VocabularyResultText.text = NotificationText.CORRECT.ToString() + "!";
            VocabularyResultText.color = Color.green;
        }
        else
        {
            VocabularyResultText.text = NotificationText.INCORRECT.ToString() + "!";
            VocabularyResultText.color = Color.red;

        }
        VocabularyResultNoti.SetActive(true);
        StartCoroutine(ActivateVocabularyResultNotification());
    }

    IEnumerator ActivateVocabularyResultNotification()
    {
        yield return new WaitForSeconds(disableNotiTime);

        VocabularyResultNoti.SetActive(false);
    }
}

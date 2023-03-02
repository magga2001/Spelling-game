using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject VocabularyResultNoti;
    [SerializeField] private TextMeshProUGUI VocabularyResultText;

    [SerializeField] private float disableNotiTime;

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

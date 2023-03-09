using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private GameObject story;
    [SerializeField] private LevelLoader transition;

    private void Start()
    {
        StartCoroutine(StartStory());
    }

    IEnumerator StartStory()
    {
        yield return new WaitForSeconds(5);

        story.gameObject.SetActive(true);

        yield return new WaitForSeconds(5);

        transition.LoadingLevel(SceneManager.GetActiveScene().buildIndex + 1);

    }
}

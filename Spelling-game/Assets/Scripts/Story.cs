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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            SkipStory();
        }
    }

    public void SkipStory()
    {
        SceneManager.LoadScene(SceneName.sceneOne);
    }

    IEnumerator StartStory()
    {
        yield return new WaitForSeconds(2);

        story.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        transition.LoadingLevel(SceneManager.GetActiveScene().buildIndex + 1);

    }
}

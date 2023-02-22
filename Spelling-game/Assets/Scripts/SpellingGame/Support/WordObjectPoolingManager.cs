using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordObjectPoolingManager : MonoBehaviour
{
    private static WordObjectPoolingManager instance;
    public static WordObjectPoolingManager Instance { get { return instance; } }

    [SerializeField] private GameObject wordPrefab;

    [SerializeField] private int wordAmount;


    private List<GameObject> words;


    void Awake()
    {
        instance = this;

        //Preload letters
        words = new List<GameObject>(wordAmount);
        for (int i = 0; i < wordAmount; i++)
        {
            GameObject prefabInstance = Instantiate(wordPrefab);
            //So the prefabInstance will be under this ObjectPooling Manager for organisation
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            words.Add(prefabInstance);
        }
    }

    public GameObject GetWord(string word)
    {
        foreach (GameObject currentWord in words)
        {
            if (!currentWord.activeInHierarchy)
            {
                currentWord.SetActive(true);
                currentWord.GetComponent<WordBox>().Word = word;
                currentWord.GetComponent<WordBox>().SetWordBoxes();
                return currentWord;
            }
        }
        GameObject prefabInstance = Instantiate(wordPrefab);
        //so the prefabInstance will be under this ObjectPooling Manager for organisation
        prefabInstance.transform.SetParent(transform);
        prefabInstance.GetComponent<WordBox>().Word = word;
        prefabInstance.GetComponent<WordBox>().SetWordBoxes();
        words.Add(prefabInstance);
        return prefabInstance;
    }
}

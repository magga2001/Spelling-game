using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AlphabetData;

public class LetterObjectPoolingManager : MonoBehaviour
{
    private static LetterObjectPoolingManager instance;
    public static LetterObjectPoolingManager Instance { get { return instance; } }

    [SerializeField] private GameObject letterPrefab;

    [SerializeField] private int letterAmount;


    private List<GameObject> letters;


    void Awake()
    {
        instance = this;

        //Preload letters
        letters = new List<GameObject>(letterAmount);
        for (int i = 0; i < letterAmount; i++)
        {
            GameObject prefabInstance = Instantiate(letterPrefab);
            //So the prefabInstance will be under this ObjectPooling Manager for organisation
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            letters.Add(prefabInstance);
        }
    }

    public GameObject GetLetter(
        string letter_name, 
        Sprite default_alphabet,
        Sprite selected_alphabet,
        Sprite correct_alphabet,
        Sprite wrong_alphabet, 
        int col, 
        int row)
    {
        foreach (GameObject letter in letters)
        {
            if (!letter.activeInHierarchy)
            {
                letter.SetActive(true);
                letter.GetComponent<LetterBox>().SetLetter(letter_name);
                letter.GetComponent<LetterBox>().SetSprites(
                    default_alphabet,
                    selected_alphabet,
                    correct_alphabet,
                    wrong_alphabet);
                letter.GetComponent<LetterBox>().SetPosition(col, row);
                letter.GetComponent<LetterBox>().Selected = false;
                return letter;
            }
        }
        GameObject prefabInstance = Instantiate(letterPrefab);
        //so the prefabInstance will be under this ObjectPooling Manager for organisation
        prefabInstance.transform.SetParent(transform);
        prefabInstance.GetComponent<LetterBox>().SetLetter(letter_name);
        prefabInstance.GetComponent<LetterBox>().SetSprites(
            default_alphabet,
            selected_alphabet,
            correct_alphabet,
            wrong_alphabet);
        prefabInstance.GetComponent<LetterBox>().SetPosition(col, row);
        prefabInstance.GetComponent<LetterBox>().Selected = false;
        letters.Add(prefabInstance);
        return prefabInstance;
    }
}

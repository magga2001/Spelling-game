using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public void LoadingLevel(int index)
    {
        SceneManager.LoadScene(index);
        //StartCoroutine(LoadLevel(index));
    }
}


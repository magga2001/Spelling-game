using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseCanvas;

    public GameObject mainCanvas;

    public GameObject spellingCanvas;

    public LevelLoader levelLoader;

    private void Start()
    {

        mainCanvas.SetActive(false);
    }

    void Update()
    {

        if (GameIsPaused)
        {
            spellingCanvas.SetActive(false);
            pauseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseCanvas.SetActive(false);
            spellingCanvas.SetActive(true);
            Time.timeScale = 1f;


        }

        if (GameManager.Instance.GameIsOver)
        {
            mainCanvas.SetActive(false);
        }
        else
        {
            mainCanvas.SetActive(true);
        }

    }

    public void Resume()
    {
        mainCanvas.SetActive(false);
        GameIsPaused = false;
    }

    public void Pause()
    {

        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        GameIsPaused = false;
        levelLoader.LoadingLevel(0);
    }

    public void RestartGame()
    {
        //AudioManager.instance.Play("ButtonClick");

        GameIsPaused = false;
        SceneManager.LoadScene(SceneName.sceneOne);


    }

    private void OnApplicationFocus(bool focus)
    {
        //SaveManager.SavePlayerInfo();

        if (focus == false && !GameManager.Instance.GameIsOver)
        {   
            //GameIsPaused = true;
        }
    }

}
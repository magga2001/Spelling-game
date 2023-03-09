using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private TextMeshProUGUI scoreUI;

    private void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreUI.text = scoreSystem.GetScore().ToString();
    }
}

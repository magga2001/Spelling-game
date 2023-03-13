using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class HighScores : ScriptableObject
{
    [SerializeField] private List<HighScoreData> highScores;

    public List<HighScoreData> GetHighScoreDatas() => highScores;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameMode : ScriptableObject
{
    [SerializeField] private bool isEndless;
    [SerializeField] private SpellingGames game;
    [SerializeField] private Difficulties difficulties;

    public bool IsEndless { get { return isEndless; } set { isEndless = value; } }
    public SpellingGames Game { get { return game; } set { game = value; } }
    public Difficulties Difficulties { get { return difficulties; } set { difficulties = value; } }
}

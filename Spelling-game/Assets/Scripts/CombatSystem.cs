using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;
using TMPro;

public class CombatSystem : MonoBehaviour
{
    private static CombatSystem instance;
    public static CombatSystem Instance { get { return instance; } }

    [SerializeField] private SpellingDifficultiesManager spellingDifficultiesManager;

    private GameObject currentEnemy;

    private void Awake()
    {
        instance = this;
    }

    public void SetUpCombat(Transform enemySpawnLocation)
    {
        currentEnemy = ObjectPoolingManager.Instance.GetEnemy(spellingDifficultiesManager.Difficulties, enemySpawnLocation);
        currentEnemy.GetComponent<Enemy>().Weapon.StartAttacking();
    }

    public bool CheckIsCombatSession()
    {
        //foreach (Transform player in enemies)
        //{
        //if (player.gameObject.activeSelf)
        //{
        //continue;
        //}
        //}

        //return false;

        if (currentEnemy != null)
        {
            return !currentEnemy.activeSelf;
        }

        return false;
    }

    public void EndCombat()
    {
        GameManager.Instance.ContinueToNextOpponent();
    }
}

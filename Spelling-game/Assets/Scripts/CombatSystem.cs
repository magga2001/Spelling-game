using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;
using TMPro;

public class CombatSystem : Subject<GameEvent>
{
    [SerializeField] private SpellingDifficultiesManager spellingDifficultiesManager;

    private GameObject currentEnemy;

    public void SetUpCombat(Transform enemySpawnLocation)
    {
        currentEnemy = ObjectPoolingManager.Instance.GetEnemy(spellingDifficultiesManager.Difficulties, enemySpawnLocation);
        currentEnemy.GetComponent<Enemy>().Weapon.StartAttacking();
    }

    public void SetUpBossCombat(Transform enemySpawnLocation)
    {
        currentEnemy = ObjectPoolingManager.Instance.GetBoss(enemySpawnLocation);
        currentEnemy.GetComponent<Enemy>().Weapon.StartAttacking();
    }

    public bool CheckIsCombatSession()
    {
        if (currentEnemy != null)
        {
            return !currentEnemy.activeSelf;
        }

        return false;
    }
}

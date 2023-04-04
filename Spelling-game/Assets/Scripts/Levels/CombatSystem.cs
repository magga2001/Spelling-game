using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;
using TMPro;

// Setting up the combat between every enemy
public class CombatSystem : Subject<GameEvent>
{
    [SerializeField] private SpellingDifficultiesManager spellingDifficultiesManager;

    private GameObject currentEnemy;


    //Set up combat with normal enemy
    //Spawn the correct difficulty of enemy at the assigned location
    //Notify the enemy to start attacking
    public void SetUpCombat(Transform enemySpawnLocation)
    {
        currentEnemy = ObjectPoolingManager.Instance.GetEnemy(spellingDifficultiesManager.Difficulties, enemySpawnLocation);
        currentEnemy.GetComponent<Enemy>().Weapon.StartAttacking();
    }


    //Set up combat with boss
    //Spawn the enemy at assigned location
    //Notify the boss to start attacking
    public void SetUpBossCombat(Transform enemySpawnLocation)
    {
        currentEnemy = ObjectPoolingManager.Instance.GetBoss(enemySpawnLocation);
        currentEnemy.GetComponent<Enemy>().Weapon.StartAttacking();
    }

    //Check if all the enemies die in the current combat fight
    public bool CheckIsCombatSession()
    {
        if (currentEnemy != null)
        {
            return !currentEnemy.activeSelf;
        }

        return false;
    }
}

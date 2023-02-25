using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    private static CombatSystem instance;
    public static CombatSystem Instance { get { return instance; } }

    [SerializeField] private Weapon playerWeapon;

    private int sessionXP;
    private float sessionTime;
    private GameObject currentEnemy;

    private void Awake()
    {
        instance = this;    
    }

    public void SetUpCombat(GameObject enemy)
    {
        sessionXP = 0;
        currentEnemy = enemy;
        currentEnemy.GetComponent<Enemy>().Weapon.StartAttacking();
    }

    private void Update()
    {
        sessionTime += Time.deltaTime;
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

    public void IncreaseSessionXP(int newXP)
    {
        playerWeapon.Fire();
        sessionXP += newXP;
    }

    public int GetTotalXP()
    {
        sessionXP -= (int) sessionTime;

        return sessionXP;
    }
}

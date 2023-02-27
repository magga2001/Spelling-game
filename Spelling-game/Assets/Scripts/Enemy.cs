using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int maxHealth = 100;

    [SerializeField] private EnemyWeapon weapon;

    public EnemyWeapon Weapon { get { return weapon; } set { weapon = value; } }

    private int currentHealth;

    // Start is called before the first frame update
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy has died");

        gameObject.SetActive(false);

        //CombatSystem.Instance.EndCombat();

        //Spawn dying effect
        //Audio dying effect

        //Maybe spawn a coin?
    }
}

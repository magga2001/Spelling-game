using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : Subject<(GameEvent gameEvent, EnemyData enemy)>
{

    [SerializeField] private int maxHealth;

    [SerializeField] private EnemyWeapon weapon;

    [SerializeField] private int reward;

    [SerializeField] private EnemyHealthBar healthBar;

    public EnemyWeapon Weapon { get { return weapon; } set { weapon = value; } }

    private int currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        NotifyObservers((GameEvent.ENEMY_DIE, new (this.name, this.gameObject, reward)));

        //Instantiate dying effect
        GameObject effect = EffectObjectPoolingManager.Instance.GetExplosionEffect();
        effect.transform.position = transform.position;
    }
}

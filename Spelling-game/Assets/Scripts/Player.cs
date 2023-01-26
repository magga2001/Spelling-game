using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    [SerializeField] private int life;
    [SerializeField] private HealthBar healthBar;
    private int currentHealth;

    public int Life { get { return life; } set { life = value; } }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if(life <= 0)
        {
            //Set the game active false
            //Spawn death effect
        }
        else
        {
            life--;
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
    }
}

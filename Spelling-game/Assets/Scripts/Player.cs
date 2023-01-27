using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    [SerializeField] private int lives;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Life life;
    private int currentHealth;

    public int Lives { get { return lives; } set { lives = value; } }

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

    public void IncreaseHealth(int health)
    {
        currentHealth += health;
        healthBar.SetHealth(currentHealth);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void Die()
    {
        lives--;
        currentHealth = 0;
        if (lives <= 0)
        {
            life.updateLife(lives);
            Debug.Log("Gameover");
            //Set the game active false
            //Spawn death effect
        }
        else
        {
            Debug.Log("DIED");
            life.updateLife(lives);
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }                                           
    }
}

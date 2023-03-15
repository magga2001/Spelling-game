using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    [HideInInspector][SerializeField] private int lives;
    [SerializeField] private int maxLives;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Life life;
    [HideInInspector][SerializeField] private int currentHealth;

    public int CurrentHealth { get { return currentHealth; } }
    public int Lives { get { return lives; } set { lives = value; } }

    // Start is called before the first frame update
    void Start()
    {
        //InGameSaveManager.DeleteProgess();
        InGameData data = InGameSaveManager.LoadInGameInfo();
        try
        {
            currentHealth = data.health;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
            life.SetUp(maxLives, data.lives);
        }
        catch (Exception)
        {
            life.SetUp(maxLives, lives);
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
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
            life.UpdateLife(lives);
            GameManager.Instance.GameIsOver = true;
            Debug.Log("Gameover");
            //Set the game active false
            //Spawn death effect
        }
        else
        {
            Debug.Log("DIED");
            life.UpdateLife(lives);
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
        }                                           
    }
}

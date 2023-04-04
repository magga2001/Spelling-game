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
        //Load the data so far every time a scene start in the current game session
        InGameData data = InGameSaveManager.LoadInfo();
        try
        {
            currentHealth = data.Health();
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
            life.SetUp(maxLives, data.Score());
        }
        catch (Exception)
        {
            //If this is the first scene after entering game session, then set initial state
            //for the player
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
        //If no more lives left, then game is over
        if (lives <= 0)
        {
            life.UpdateLife(lives);
            GameManager.Instance.GameIsOver = true;
            Debug.Log("Gameover");
        }
        //Otherwise, substract life and refill the health
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

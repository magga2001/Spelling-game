using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float lifeDuration;
    [SerializeField] private GameObject playerBulletEffect;
    [SerializeField] private GameObject enemyBulletEffect;
    private float lifeTimer;
    private bool shotByPlayer;
    public bool ShotByPlayer { get { return shotByPlayer; } set { shotByPlayer = value; } }

    private int damage;

    void OnEnable()
    {
        //Reset lifetime on the bullet
        lifeTimer = lifeDuration;
        Effect();
    }

    void Update()
    {
        //Bullet move towards the direction it shoots from
        transform.position += transform.up * speed * Time.deltaTime;
        DecreaseLifeTime();
        Effect();
    }

    private void DecreaseLifeTime()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    public void DamageSetUp(int damage)
    {
        this.damage = damage;
    }

    //Bullet effect for player and enemy to
    //differentiate the shot taken by player or enemy
    private void Effect()
    {
        if (ShotByPlayer)
        {
            enemyBulletEffect.SetActive(false);
            playerBulletEffect.SetActive(true);
        }
        else
        {
            playerBulletEffect.SetActive(false);
            enemyBulletEffect.SetActive(true);


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the collided object is enemy
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null && ShotByPlayer)
        {
            enemy.TakeDamage(damage);
            AudioManager.instance.Play("Hit");
            gameObject.SetActive(false);
        }


        //Check if the collided object is player 
        Player player = collision.GetComponent<Player>();
        if(player != null && !ShotByPlayer)
        {
            player.TakeDamage(damage);
            AudioManager.instance.Play("Hit");
            gameObject.SetActive(false);
        }
    }
}

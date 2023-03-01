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

    // Start is called before the first frame update
    void OnEnable()
    {
        lifeTimer = lifeDuration;
        Effect();
    }

    // Update is called once per frame
    void Update()
    {
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
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null && ShotByPlayer)
        {
            enemy.TakeDamage(damage);
            gameObject.SetActive(false);

            //Audio hits enemy
            //Spawn enemy effect
            //Set game object to false
        }

        Player player = collision.GetComponent<Player>();
        if(player != null && !ShotByPlayer)
        {
            player.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}

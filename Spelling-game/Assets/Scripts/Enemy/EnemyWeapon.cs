using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnemyWeapon : MonoBehaviour
{
    private Transform player;

    [SerializeField] private int damage;

    [SerializeField] private Transform[] firePoints;

    [SerializeField] private float fireDelay;
    [SerializeField] private float initialDelay;
    [SerializeField] private bool canShoot;

    private bool activate;
    private float delay;
    private bool shoot;
    private void OnEnable()
    {
        //Reset the enemy's state
        activate = false;
        delay = initialDelay;
        shoot = canShoot;

        try
        {
            //Shoot towards the player and check the player existent in the game
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        catch
        {
            Debug.Log("Player dying");
        }
    }

    void Update()
    {
        if(activate)
        {
            //So that the enemy wont start shooting as soon as it appeared
            delay -= Time.deltaTime;
            Shoot();
        }
    }

    public void StartAttacking()
    {
        activate = true;
    }

    private void Shoot()
    {
        if (shoot && player != null && delay < 0)
        {
            shoot = false;

            // Shoot toward the player
            for (int i = 0; i < firePoints.Length; i++)
            {
                GameObject bullet = ObjectPoolingManager.Instance.GetBullet(false, damage);
                bullet.transform.position = firePoints[i].transform.position;
                bullet.transform.right = firePoints[i].transform.up;
                AudioManager.instance.Play("Shoot");
                GameObject effect = EffectObjectPoolingManager.Instance.GetEnemyShootingEffect();
                effect.transform.position = firePoints[i].transform.position;
            }

            // Call the IEnumerator method
            StartCoroutine(FirePause());
        }

    }

    IEnumerator FirePause()
    {
        //Wait for a few seconds
        yield return new WaitForSeconds(fireDelay);

        //Shoot is now available
        shoot = true;
    }


}
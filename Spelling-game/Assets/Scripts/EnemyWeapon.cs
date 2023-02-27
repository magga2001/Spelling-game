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
        activate = false;
        delay = initialDelay;
        shoot = canShoot;

        try
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        catch
        {
            Debug.Log("Player dying");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(activate)
        {
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

            for (int i = 0; i < firePoints.Length; i++)
            {
                GameObject bullet = ObjectPoolingManager.Instance.GetBullet(false, damage);
                bullet.transform.position = firePoints[i].transform.position;
                bullet.transform.right = firePoints[i].transform.up;
            }

            StartCoroutine(FirePause());
        }

    }

    IEnumerator FirePause()
    {
        yield return new WaitForSeconds(fireDelay);

        shoot = true;

    }


}
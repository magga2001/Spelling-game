using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnemyWeapon : MonoBehaviour
{
    private Transform player;

    [SerializeField] private int damage;

    [SerializeField] private Transform[] firePoints;

    public float fireDelay = 2f;
    public float initialDelay = 5f;
    public bool canShoot = true;

    private bool coroutineAllowed;
    // Start is called before the first frame update

    void Start()
    {
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

        initialDelay -= Time.deltaTime;
        Shoot();
    }

    private void Shoot()
    {
        if (canShoot && player != null && initialDelay < 0)
        {
            canShoot = false;

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

        canShoot = true;

    }


}
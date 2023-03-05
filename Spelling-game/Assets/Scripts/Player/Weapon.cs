using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    [SerializeField] private int damage;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = ObjectPoolingManager.Instance.GetBullet(true, damage);
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.right = -firePoint.transform.up;
        }
    }

    public void Fire()
    {
        GameObject bullet = ObjectPoolingManager.Instance.GetBullet(true, damage);
        bullet.transform.position = firePoint.transform.position;
        bullet.transform.right = -firePoint.transform.up;
    }
}


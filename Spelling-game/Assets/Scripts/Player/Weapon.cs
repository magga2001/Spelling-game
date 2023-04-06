using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    [SerializeField] private int damage;

    public void Fire()
    {
        //Get a bullet from object pooling manager and shoot toward the direction the weapon is facing
        GameObject bullet = ObjectPoolingManager.Instance.GetBullet(true, damage);
        bullet.transform.position = firePoint.transform.position;
        bullet.transform.right = -firePoint.transform.up;
        AudioManager.instance.Play("Shoot");
        GameObject effect = EffectObjectPoolingManager.Instance.GetPlayerShootingEffect();
        effect.transform.position = firePoint.transform.position;
    }
}


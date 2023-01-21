using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager instance;
    public static ObjectPoolingManager Instance { get { return instance; } }

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private int bulletAmount = 10;
    [SerializeField] private int enemyAmount = 10;


    private List<GameObject> bullets;
    private List<GameObject> enemies;


    void Awake()
    {
        instance = this;

        //Preload bullets
        bullets = new List<GameObject>(bulletAmount);
        for(int i = 0; i < bulletAmount; i++)
        {
            GameObject prefabInstance = Instantiate(bulletPrefab);
            //So the prefabInstance will be under this ObjectPooling Manager for organisation
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            bullets.Add(prefabInstance);
        }

        //Preload enemies
        enemies = new List<GameObject>(enemyAmount);
        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject prefabInstance = Instantiate(enemyPrefab);
            //So the prefabInstance will be under this ObjectPooling Manager for organisation
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            enemies.Add(prefabInstance);
        }
    }

    public GameObject GetBullet(bool shotByPlayer, int damage)
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().ShotByPlayer = shotByPlayer;
                bullet.GetComponent<Bullet>().DamageSetUp(damage);
                return bullet;
            }
        }
        GameObject prefabInstance = Instantiate(bulletPrefab);
        //so the prefabInstance will be under this ObjectPooling Manager for organisation
        prefabInstance.transform.SetParent(transform);
        prefabInstance.GetComponent<Bullet>().ShotByPlayer = shotByPlayer;
        prefabInstance.GetComponent<Bullet>().DamageSetUp(damage);
        bullets.Add(prefabInstance);
        return prefabInstance;
    }

    public GameObject GetEnemy()
    {
        foreach (GameObject enemy in enemies)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }
        GameObject prefabInstance = Instantiate(enemyPrefab);
        //so the prefabInstance will be under this ObjectPooling Manager for organisation
        prefabInstance.transform.SetParent(transform);
        enemies.Add(prefabInstance);
        return prefabInstance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager instance;
    public static ObjectPoolingManager Instance { get { return instance; } }

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject easyEnemyPrefab;
    [SerializeField] private GameObject mediumEnemyPrefab;
    [SerializeField] private GameObject hardEnemyPrefab;
    [SerializeField] private GameObject bossPrefab;

    [SerializeField] private int bulletAmount = 10;
    [SerializeField] private int easyEnemyAmount = 1;
    [SerializeField] private int mediumEnemyAmount = 1;
    [SerializeField] private int hardEnemyAmount = 1;
    [SerializeField] private int bossAmount = 1;


    private List<GameObject> bullets;
    private List<GameObject> easyEnemies;
    private List<GameObject> mediumEnemies;
    private List<GameObject> hardEnemies;
    private List<GameObject> bosses;


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
        easyEnemies = new List<GameObject>(easyEnemyAmount);
        for (int i = 0; i < easyEnemyAmount; i++)
        {
            GameObject prefabInstance = Instantiate(easyEnemyPrefab);
            //So the prefabInstance will be under this ObjectPooling Manager for organisation
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            easyEnemies.Add(prefabInstance);
        }

        mediumEnemies = new List<GameObject>(mediumEnemyAmount);
        for (int i = 0; i < mediumEnemyAmount; i++)
        {
            GameObject prefabInstance = Instantiate(mediumEnemyPrefab);
            //So the prefabInstance will be under this ObjectPooling Manager for organisation
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            mediumEnemies.Add(prefabInstance);
        }

        hardEnemies = new List<GameObject>(hardEnemyAmount);
        for (int i = 0; i < hardEnemyAmount; i++)
        {
            GameObject prefabInstance = Instantiate(hardEnemyPrefab);
            //So the prefabInstance will be under this ObjectPooling Manager for organisation
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            hardEnemies.Add(prefabInstance);
        }

        bosses = new List<GameObject>(bossAmount);
        for (int i = 0; i < bossAmount; i++)
        {
            GameObject prefabInstance = Instantiate(bossPrefab);
            //So the prefabInstance will be under this ObjectPooling Manager for organisation
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            bosses.Add(prefabInstance);
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

    public GameObject GetEnemy(Difficulties difficulties, Transform enemySpawnLocation)
    {

        if(difficulties == Difficulties.EASY)
        {
            foreach (GameObject enemy in easyEnemies)
            {
                if (!enemy.activeInHierarchy)
                {
                    enemy.SetActive(true);
                    enemy.transform.position = enemySpawnLocation.position;
                    return enemy;
                }
            }
            GameObject prefabInstance = Instantiate(easyEnemyPrefab);
            //so the prefabInstance will be under this ObjectPooling Manager for organisation
            prefabInstance.transform.SetParent(transform);
            prefabInstance.transform.position = enemySpawnLocation.position;
            easyEnemies.Add(prefabInstance);
            return prefabInstance;
        }
        else if (difficulties == Difficulties.MEDIUM)
        {
            foreach (GameObject enemy in mediumEnemies)
            {
                if (!enemy.activeInHierarchy)
                {
                    enemy.SetActive(true);
                    enemy.transform.position = enemySpawnLocation.position;
                    return enemy;
                }
            }
            GameObject prefabInstance = Instantiate(mediumEnemyPrefab);
            //so the prefabInstance will be under this ObjectPooling Manager for organisation
            prefabInstance.transform.SetParent(transform);
            prefabInstance.transform.position = enemySpawnLocation.position;
            mediumEnemies.Add(prefabInstance);
            return prefabInstance;
        }
        else 
        {
            foreach (GameObject enemy in hardEnemies)
            {
                if (!enemy.activeInHierarchy)
                {
                    enemy.SetActive(true);
                    enemy.transform.position = enemySpawnLocation.position;
                    return enemy;
                }
            }
            GameObject prefabInstance = Instantiate(hardEnemyPrefab);
            //so the prefabInstance will be under this ObjectPooling Manager for organisation
            prefabInstance.transform.SetParent(transform);
            prefabInstance.transform.position = enemySpawnLocation.position;
            hardEnemies.Add(prefabInstance);
            return prefabInstance;
        }
    }

    public GameObject GetBoss(Transform enemySpawnLocation)
    {
        foreach (GameObject enemy in bosses)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                enemy.transform.position = enemySpawnLocation.position;
                return enemy;
            }
        }
        GameObject prefabInstance = Instantiate(bossPrefab);
        //so the prefabInstance will be under this ObjectPooling Manager for organisation
        prefabInstance.transform.SetParent(transform);
        prefabInstance.transform.position = enemySpawnLocation.position;
        bosses.Add(prefabInstance);
        return prefabInstance;
    }
}

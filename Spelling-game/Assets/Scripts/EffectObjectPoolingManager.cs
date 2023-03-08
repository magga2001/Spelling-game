using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObjectPoolingManager : MonoBehaviour
{
    private static EffectObjectPoolingManager instance;
    public static EffectObjectPoolingManager Instance { get { return instance; } }

    [SerializeField] private GameObject playerShootingEffectPrefab;
    [SerializeField] private GameObject enemyShootingEffectPrefab;
    [SerializeField] private GameObject explosionEffectPrefab;

    [SerializeField] private int playerShootingEffectAmount = 5;
    [SerializeField] private int enemyShootingEffectAmount = 5;
    [SerializeField] private int explosionEffectAmount = 5;

    private List<GameObject> playerShootingEffects;
    private List<GameObject> enemyShootingEffects;
    private List<GameObject> explosionEffects;


    void Awake()
    {
        instance = this;

        playerShootingEffects = new List<GameObject>(playerShootingEffectAmount);
        for (int i = 0; i < playerShootingEffectAmount; i++)
        {
            GameObject prefabInstance = Instantiate(playerShootingEffectPrefab);
            //So the prefabInstance will be under this ObjectPooling Manager for organisation
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);
            playerShootingEffects.Add(prefabInstance);
        }

        enemyShootingEffects = new List<GameObject>(enemyShootingEffectAmount);
        for (int i = 0; i < enemyShootingEffectAmount; i++)
        {
            GameObject prefabInstance = Instantiate(enemyShootingEffectPrefab);
            //So the prefabInstance will be under this ObjectPooling Manager for organisation
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);


            enemyShootingEffects.Add(prefabInstance);
        }

        explosionEffects = new List<GameObject>(explosionEffectAmount);
        for (int i = 0; i < explosionEffectAmount; i++)
        {
            GameObject prefabInstance = Instantiate(explosionEffectPrefab);
            //So the prefabInstance will be under this ObjectPooling Manager for organisation
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            explosionEffects.Add(prefabInstance);
        }
    }

    public GameObject GetPlayerShootingEffect()
    {
        foreach (GameObject effect in playerShootingEffects)
        {
            if (!effect.activeInHierarchy)
            {
                effect.SetActive(true);
                StartCoroutine(DeactivateEffect(effect));
                return effect;
            }
        }
        GameObject prefabInstance = Instantiate(playerShootingEffectPrefab);
        //so the prefabInstance will be under this ObjectPooling Manager for organisation
        prefabInstance.transform.SetParent(transform);
        playerShootingEffects.Add(prefabInstance);
        StartCoroutine(DeactivateEffect(prefabInstance));
        return prefabInstance;
    }

    public GameObject GetEnemyShootingEffect()
    {
        foreach (GameObject effect in enemyShootingEffects)
        {
            if (!effect.activeInHierarchy)
            {
                effect.SetActive(true);
                StartCoroutine(DeactivateEffect(effect));
                return effect;
            }
        }
        GameObject prefabInstance = Instantiate(enemyShootingEffectPrefab);
        //so the prefabInstance will be under this ObjectPooling Manager for organisation
        prefabInstance.transform.SetParent(transform);
        enemyShootingEffects.Add(prefabInstance);
        StartCoroutine(DeactivateEffect(prefabInstance));
        return prefabInstance;
    }

    public GameObject GetExplosionEffect()
    {
        foreach (GameObject effect in explosionEffects)
        {
            if (!effect.activeInHierarchy)
            {
                effect.SetActive(true);
                StartCoroutine(DeactivateEffect(effect));
                return effect;
            }
        }
        GameObject prefabInstance = Instantiate(explosionEffectPrefab);
        //so the prefabInstance will be under this ObjectPooling Manager for organisation
        prefabInstance.transform.SetParent(transform);
        explosionEffects.Add(prefabInstance);
        StartCoroutine(DeactivateEffect(prefabInstance));
        return prefabInstance;
    }

    IEnumerator DeactivateEffect(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);

    }
}

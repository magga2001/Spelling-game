using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCrate : Crates
{
    [Header("GamePlay")]
    [SerializeField] private int health;

    public void OnEnable()
    {
        lifeTimer = lifeDuration;
    }

    private void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }

        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.IncreaseHealth(health);

            this.gameObject.SetActive(false);
        }

    }
}

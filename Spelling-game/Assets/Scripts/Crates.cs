using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crates : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected float lifeDuration;
    protected float lifeTimer;


    void OnEnable()
    {
        lifeTimer = lifeDuration;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
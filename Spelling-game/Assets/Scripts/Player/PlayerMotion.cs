using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    //Move player toward the location position
    public void NextLocation(Transform location)
    {
        transform.position = Vector2.MoveTowards(transform.position, location.position, moveSpeed * Time.deltaTime);
    }
}

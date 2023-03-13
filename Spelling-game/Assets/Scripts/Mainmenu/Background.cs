using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Background : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Transform[] transitionPosition;

    [SerializeField] GameObject transition;

    private int currentPoint;

    private bool transitioning;

    private void Start()
    {
        currentPoint = 0;
        transitioning = false;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, -transitionPosition[currentPoint].position, speed * Time.deltaTime);
        if (transform.position == -transitionPosition[currentPoint].position && !transitioning)
        {
            transitioning = true;
            StartCoroutine(StartTransition());
        }
    }

    IEnumerator StartTransition()
    {
        currentPoint++;

        if (currentPoint >= transitionPosition.Length)
        {
            transform.position = startPosition;
            currentPoint = 0;
        }

        transition.SetActive(true);

        yield return new WaitForSeconds(3);
        
        transition.SetActive(false);
        transitioning = false;  
    }
}
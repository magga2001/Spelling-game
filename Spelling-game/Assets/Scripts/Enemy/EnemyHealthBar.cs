using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This code is taken and inspired from
//Distorted Pixel Studios, D. (Director). (2020, April 30). 2D Character Hitpoints in Unity / 2023 [Video file]. Retrieved January 30, 2023, from https://www.youtube.com/watch?v=v1UGTTeQzbo
public class EnemyHealthBar : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private Slider slider;

    [SerializeField] private Gradient gradient;

    [SerializeField] private Image fill;

    public Vector3 offSet;

    private void OnEnable()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        //So that it attached to the enemy game object and adjusting offset. For example, to be above the head
        slider.transform.position = cam.WorldToScreenPoint(transform.parent.position + offSet);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }


    public void SetHealth(int health)

    {
        slider.value = health;

        //Updating the healthbar interface
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
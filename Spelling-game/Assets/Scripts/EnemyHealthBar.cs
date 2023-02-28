using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This code is taken and inspired from
//Thirslund, A. (Director). (2020, February 09). How to make a HEALTH BAR in Unity! [Video file]. Retrieved January 25, 2023, from https://www.youtube.com/watch?v=BLfNP4Sc_iA&amp;t=987s
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private Gradient gradient;

    [SerializeField] private Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        //Set healthbar to max heatlh
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)

    {
        slider.value = health;

        //Update the health bar
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StrikeSystem : ScriptableObject
{
    [SerializeField] private int maxStrike;
    private int strike;

    public void SetUp()
    {
        strike = 0;
    }
    public void IncreaseStrike()
    {
        strike++;
        if (strike >= maxStrike)
        {
            strike = 0;
            //DO something
        }
    }
}

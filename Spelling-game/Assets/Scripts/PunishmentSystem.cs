using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunishmentSystem : MonoBehaviour
{
    [SerializeField] private int maxStrike;

    private int strike;

    public void IncreaseStrike()
    {
        strike++;
        if(strike >= maxStrike)
        {
            strike = 0;
            //DO something
        }
    }
}

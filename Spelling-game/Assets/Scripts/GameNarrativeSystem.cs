using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNarrativeSystem : MonoBehaviour, IObserver<GameEvent>
{
    void Start()
    {

    }

    public void OnNotify(GameEvent data)
    {
        switch(data)
        {
            default:
                break;
        }
    }
}

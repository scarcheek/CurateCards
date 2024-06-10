using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterCard : CardBehaviour
{
    [SerializeField] private Counter[] counterList;

    public override void Play()
    {
        AddCounter(counterList);

        base.Play();
    }

}

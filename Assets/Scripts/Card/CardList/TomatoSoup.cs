using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoSoup : CardBehaviour
{
    [SerializeField] private Counter[] counterList;

    public override void Play()
    {
        AddCounter(counterList);

        base.Play();
    }
}

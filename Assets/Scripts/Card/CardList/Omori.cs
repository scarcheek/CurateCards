using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Omori : CardBehaviour
{
    [SerializeField] private int peopleAmount = 2;
    [SerializeField] private Counter[] counterList;

    public override void Play()
    {
        leaveOnDislike = true;
        AddCounter(counterList);

        base.Play();

        for (int i = 0; i < peopleAmount; i++)
        {
            VisitorSpawnPlaneScript.instance.SpawnAndAddVisitor();
        }

    }
}

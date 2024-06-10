using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPeopleCard : CardBehaviour
{
    [SerializeField] private int peopleCount = 1;
    public override void Play()
    {
        base.Play();
        for (int i = 0; i < peopleCount; i++)
        {
            VisitorSpawnPlaneScript.instance.SpawnAndAddVisitor();
        }
    }
}

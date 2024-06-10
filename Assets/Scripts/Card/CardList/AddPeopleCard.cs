using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddPeopleCard : CardBehaviour
{
    [SerializeField] private int peopleAmount = 1;
    public override void Play()
    {
        base.Play();
        for (int i = 0; i < peopleAmount; i++)
        {
            VisitorSpawnPlaneScript.instance.SpawnAndAddVisitor();
        }
    }
}

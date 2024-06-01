using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Wizards : CardBehaviour
{
    [SerializeField] private float multiplyVirusBy;

    public override void Play()
    {
        GameStateManager.instance.activeVirusCounters = (int)math.round(multiplyVirusBy * (float)GameStateManager.instance.activeVirusCounters);

        base.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : CardBehaviour
{
    [SerializeField] float valuePerVirus = 5;
    private float bonusValueAtStart = 0;

    internal override void Start()
    {
        base.Start();
        bonusValueAtStart = valuePerVirus * GameStateManager.instance.activeVirusCounters;
        cardValue += bonusValueAtStart;
        DisplayCardStats();
    }
    public override void Play()
    {
        cardValue -= bonusValueAtStart;
        cardValue += valuePerVirus * GameStateManager.instance.activeVirusCounters;
        base.Play();
        bonusValueAtStart = 0;
    }
}

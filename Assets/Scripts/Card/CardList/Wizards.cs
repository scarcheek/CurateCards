using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Wizards : CardBehaviour
{
    [SerializeField] private float multiplyVirusBy;

    public override void Play()
    {
        // 0.5 * 2 = 1; 2 +1 = 3; virus counters multiplied by 1.5
        int addingCounters = (int)math.ceil((multiplyVirusBy - 1) * (float)GameStateManager.instance.activeVirusCounters);
        Debug.Log(addingCounters + "should be added to " + GameStateManager.instance.activeVirusCounters);
        for (int i = 0; i < addingCounters; i++)
        {
            Debug.Log("adding counter #" + i);
            EventManager.EmitAddCounter(Counter.virus);
        }

        base.Play();
    }
}

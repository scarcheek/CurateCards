using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private float scoreToAchieve;
    [Range(0f, 1f)][SerializeField] private float initialScoreFactor;
    private float currentScore;
    public int activeAttackCounters = 0;
    public int activeDefenceCounters = 0;
    public int activeVirusCounters = 0;
    public static GameStateManager instance;
    public static float AttackCounterChange = 1.2f;
    public static float DefenceCounterChange = 0.83f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentScore = initialScoreFactor * scoreToAchieve;
        EventManager.AddBaseValueToGamestate += addBaseValue;
        EventManager.AddCounter += OnAddCounter;
        EventManager.RemoveCounter += OnRemoveCounter;
        EventManager.CurationDone += ReduceCountersByOne;
    }

    private void ReduceCountersByOne()
    {
        EventManager.EmitRemoveCounter(Counter.attack, Counter.defence, Counter.virus);
    }

    public void addBaseValue(float baseVal)
    {
        //TODO: Display the score somewhere in the UI
        currentScore += baseVal;
        Debug.Log("Current Score: " + currentScore);
    }

    private void OnAddCounter(Counter[] counters)
    {
        foreach (Counter counter in counters)
        {
            switch(counter)
            {
                case Counter.attack:
                    activeAttackCounters++;
                    break;
                case Counter.defence:
                    activeDefenceCounters++;
                    break;
                case Counter.virus:
                    activeVirusCounters++;
                    break;
            }
        }
    }
    private void OnRemoveCounter(Counter[] counters)
    {
        foreach (Counter counter in counters)
        {
            switch (counter)
            {
                case Counter.attack:
                    activeAttackCounters--;
                    break;
                case Counter.defence:
                    activeDefenceCounters--;
                    break;
                case Counter.virus:
                    activeVirusCounters--;
                    break;
            }
        }
        activeAttackCounters = math.max(0, activeAttackCounters);
        activeDefenceCounters = math.max(0, activeDefenceCounters);
        activeVirusCounters = math.max(0, activeVirusCounters);
    }

}

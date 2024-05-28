using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private float scoreToAchieve;
    [Range(0f, 1f)] [SerializeField] private float initialScoreFactor;
    private float currentScore;
    /// <summary>
    /// The function should return false if there are no uses left of the effect
    /// </summary>
    public Dictionary<Medium, Func<CardProps, bool>> ActiveEffects = new();

    public static GameStateManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentScore = initialScoreFactor * scoreToAchieve;
        EventManager.AddValueToGamestate += addValue;
        EventManager.CurationDone += ActiveEffects.Clear;
    }

    public void addValue(float baseVal)
    {
        //TODO: Display the score somewhere in the UI
        currentScore += baseVal;
        Debug.Log("Current Score: " + currentScore);
    }
}

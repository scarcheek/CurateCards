using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private float scoreToAchieve;
    [Range(0f, 1f)] [SerializeField] private float initialScoreFactor;
    private float currentScore;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = initialScoreFactor * scoreToAchieve;
        EventManager.AddBaseValueToGamestate += addBaseValue;
    }

    public void addBaseValue(float baseVal)
    {
        //TODO: Display the score somewhere in the UI
        currentScore += baseVal;
        Debug.Log("Current Score: " + currentScore);
    }
}

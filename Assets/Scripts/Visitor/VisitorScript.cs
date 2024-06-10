
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class VisitorScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer bodySprite;
    [SerializeField] private SpriteRenderer hatSprite;
    [SerializeField] private ScoreVisualizerScript scoreVisualizer;
    [Header("Properties")]
    [SerializeField] public float speed;
    [SerializeField] private float hatProb;
    [SerializeField] private float neutralThreshhold;
    [SerializeField] private float initialMotivation;
    [Header("DEBUG")]
    public Vector3 standPos;
    [SerializeField] private float currentMotivation;
    [SerializeField] private float hatMotivation;
    [SerializeField] private float bodyMotivation;
    [SerializeField] private float pantsMotivation;

    private VisitorAnimationController animController;

    private bool isLeaving = false;
    VisitorSpawnPlaneScript planeScript;


    void Start()
    {
        planeScript = GetComponentInParent<VisitorSpawnPlaneScript>();

        animController = GetComponentInChildren<VisitorAnimationController>();
        EventManager.ScoreCard += OnScoreCard;
        EventManager.RandomizePreferences += OnRandomizePreferences;


        hatSprite.enabled = Random.value > hatProb;
        currentMotivation = initialMotivation;
        hatMotivation = Random.Range(-30, 30);
        bodyMotivation = Random.Range(-30, 30);
        pantsMotivation = Random.Range(-30, 30);
    }

    private void OnRandomizePreferences()
    {
        hatMotivation = Random.Range(-30, 30);
        bodyMotivation = Random.Range(-30, 30);
        pantsMotivation = Random.Range(-30, 30);
    }

    private void OnScoreCard(CardBehaviour card)
    {
        float motivationChange = 0;
        float calculatedScore = CalculateScore(card, ref motivationChange);

        motivationChange = math.round(motivationChange);
        currentMotivation += motivationChange;
        Debug.Log("I got a score of " + (calculatedScore) + " with a score factor of " + motivationChange + "\n" +
            "Motivation changed by " + motivationChange + " resulting in a total motivation of: " + currentMotivation);

        EventManager.EmitAddBaseValueToGamestate(calculatedScore);
        animController.ReactToScore(neutralThreshhold, currentMotivation);
        scoreVisualizer.showScore(System.Math.Round(calculatedScore), motivationChange);
    }

    private float CalculateScore(CardBehaviour card, ref float motivationChange)
    {
        //TODO: Calculate Score based on clothing
        motivationChange = hatMotivation + bodyMotivation + pantsMotivation;
        motivationChange += Random.Range(-5, 5);
        if (card.ignoreDislike && motivationChange < 0) motivationChange = 0;
        if (card.leaveOnDislike && motivationChange < 0)
        {
            currentMotivation = 0;
            return 0; 
        }

        GameStateManager instance = GameStateManager.instance;
        motivationChange += instance.activeVirusCounters;
        motivationChange *= math.pow(GameStateManager.AttackCounterChange, instance.activeAttackCounters);
        motivationChange *= math.pow(GameStateManager.DefenceCounterChange, instance.activeDefenceCounters);
        

        return card.cardValue + motivationChange;
    }

    public void ReactionDone()
    {
        if (currentMotivation <= 0)
        {
            Leave();
        }
    }

    private void Leave()
    {
        isLeaving = true;
        standPos = planeScript.RandomPointOutBounds();
        EventManager.ScoreCard -= OnScoreCard;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, standPos) > 0.001f)
        {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, standPos, step);
        }
        else if (isLeaving)
        {
            //TODO: Remove visitor properly 
            Destroy(gameObject);
        }
    }
}

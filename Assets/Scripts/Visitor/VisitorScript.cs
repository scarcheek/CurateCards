
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    private float individualScoreFactor;

    private bool isLeaving = false;
    VisitorSpawnPlaneScript planeScript;


    void Start()
    {
        planeScript = GetComponentInParent<VisitorSpawnPlaneScript>();

        animController = GetComponentInChildren<VisitorAnimationController>();
        EventManager.ScoreCard += OnScoreCard;
        hatSprite.enabled = Random.value > hatProb;
        currentMotivation = initialMotivation;
        hatMotivation = Random.Range(-30, 30);
        bodyMotivation = Random.Range(-30, 30);
        pantsMotivation = Random.Range(-30, 30);
    }

    private float CalculateScore(CardProps card, ref float motivation)
    {
        //TODO: Calculate Score based on clothing
        individualScoreFactor = hatMotivation + bodyMotivation + pantsMotivation + Random.Range(-5, 5);
        motivation += individualScoreFactor;
        //Debug.Log("I got a score of " + card.baseValue + individualScoreFactor + " with a score factor of " + individualScoreFactor);
        return card.baseValue + individualScoreFactor;
    }

    public void ReactionDone()
    {
        if (currentMotivation < 0)
        {
            isLeaving = true;
            standPos = planeScript.RandomPointOutBounds();
            EventManager.ScoreCard -= OnScoreCard;
        }
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
    private void OnScoreCard(CardProps card)
    {
        float motivationChange = 0;
        float calculatedScore = CalculateScore(card, ref motivationChange);
        currentMotivation += motivationChange;
        EventManager.EmitAddBaseValueToGamestate(calculatedScore);
        animController.ReactToScore(neutralThreshhold, currentMotivation);
        scoreVisualizer.showScore(System.Math.Round(calculatedScore), motivationChange);
    }
}

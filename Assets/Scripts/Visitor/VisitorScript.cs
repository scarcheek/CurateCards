using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer bodySprite;
    [SerializeField] private SpriteRenderer hatSprite;
    [Header("Properties")]
    [SerializeField] public float speed;
    [SerializeField] private float hatProb;
    [SerializeField] private float neutralThreshhold;
    [SerializeField] private float initialMotivation;
    [Header("DEBUG")]
    public Vector3 standPos;

    private VisitorAnimationController animController;
    private float individualScoreFactor;
    private float currentMotivation;

    private bool isLeaving = false;
    VisitorSpawnPlaneScript planeScript;


    void Start()
    {
        planeScript = GetComponentInParent<VisitorSpawnPlaneScript>();

        animController = GetComponentInChildren<VisitorAnimationController>();
        EventManager.ScoreCard += OnScoreCard;
        hatSprite.enabled = Random.value > hatProb;
        currentMotivation = initialMotivation;
    }

    private float calculateScore(CardProps card)
    {
        //TODO: Calculate Score based on clothing
        individualScoreFactor = Random.Range(-.8f, 1f);
        individualScoreFactor += Random.Range(-.8f, 1f);
        individualScoreFactor += Random.Range(-.8f, 1f);
        Debug.Log("I got a score of " + card.baseValue * individualScoreFactor + " with a score factor of " + individualScoreFactor);
        return card.baseValue * individualScoreFactor;
    }

    public void ReactionDone()
    {
        if(currentMotivation < 0)
        {
            isLeaving = true;
            standPos = planeScript.RandomPointOutBounds();
            EventManager.ScoreCard -= OnScoreCard;
        }
    }

    private void OnScoreCard(CardProps card)
    {
        float calculatedScore = calculateScore(card);
        currentMotivation += calculatedScore;
        EventManager.EmitAddBaseValueToGamestate(calculatedScore);
        animController.ReactToScore(neutralThreshhold, calculatedScore);
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

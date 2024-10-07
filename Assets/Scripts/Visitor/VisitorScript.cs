
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class VisitorScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MeshRenderer bodyRenderer;
    [SerializeField] private MeshRenderer hatRenderer;
    [SerializeField] private MeshRenderer pantsRenderer;
    [SerializeField] private ScoreVisualizerScript scoreVisualizer;
    [Header("Properties")]
    [SerializeField] public float speed;
    [SerializeField] private float neutralThreshhold;
    [SerializeField] private float initialMotivation;
    [Header("DEBUG")]
    public Vector3 standPos;
    [SerializeField] private float currentMotivation;
    [SerializeField] private ClothingProps hat;
    [SerializeField] private ClothingProps body;
    [SerializeField] private ClothingProps pants;

    private VisitorAnimationController animController;
    private ClothingManager clothingManager;

    private float calculatedScore = 0f;
    private bool isLeaving = false;
    private bool beenHit = false;
    VisitorSpawnPlaneScript planeScript;


    void Start()
    {
        planeScript = GetComponentInParent<VisitorSpawnPlaneScript>();
        clothingManager = GameStateManager.instance.GetComponent<ClothingManager>();

        animController = GetComponentInChildren<VisitorAnimationController>();
        EventManager.ScoreCard += OnScoreCard;
        EventManager.RandomizePreferences += OnRandomizePreferences;
        EventManager.PresentCard += ResetCalculatedScore;

        currentMotivation = initialMotivation;
        OnRandomizePreferences();
        ClothingSpritesUpdate();
        
    }



    private void ResetCalculatedScore(PlayingCardScript card)
    {
        calculatedScore = 0;
    }


    private void OnRandomizePreferences()
    {
        hat = clothingManager.randomHat();
        body = clothingManager.randomBody();
        pants = clothingManager.randomPants();
    }

    private void ClothingSpritesUpdate()
    {
        bodyRenderer.material = body.material;
        pantsRenderer.material = pants.material;
        hatRenderer.material = hat.material;
    }

    private void OnScoreCard(CardBehaviour card)
    {
        float motivationChange = 0;
        calculatedScore = CalculateScore(card, ref motivationChange);

        motivationChange = math.round(motivationChange);
        //Debug.Log("I got a score of " + (calculatedScore) + " with a score factor of " + motivationChange + "\n" +
        //    "Motivation changed by " + motivationChange + " resulting in a total motivation of: " + currentMotivation);

        EventManager.EmitAddBaseValueToGamestate(calculatedScore);

        if (!isLeaving && !beenHit)
        {
            animController.ReactToScore(neutralThreshhold, currentMotivation);
            scoreVisualizer.showScore(System.Math.Round(calculatedScore), motivationChange);
        }
    }

    private float CalculateScore(CardBehaviour card, ref float motivationChange)
    {

        int typeCount = 0;
        foreach (CardType type in card.cardProps.cardType)
        {
            if(type != CardType.ANY) {
                typeCount++;
                motivationChange += hat.prefernces[type] + body.prefernces[type] + pants.prefernces[type];
            }
            
        }
        if (typeCount > 1) {
            motivationChange = motivationChange / typeCount;
        }
        
        
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
        if (!isLeaving && currentMotivation <= 0) 
        {
            Debug.Log("Leaving with: " + currentMotivation + " " + beenHit);
            Leave();
        }
    }

    public void OnFallOverBackDone()
    {
        Debug.Log("Dei´mama is so fett doss i geh");
        if (!isLeaving) Leave();
    }


    private void Leave()
    {
        isLeaving = true;
        

        if (beenHit)
        {
            EventManager.EmitAddBaseValueToGamestate(-calculatedScore);
            scoreVisualizer.showScore(System.Math.Round(-calculatedScore), -1);
        }
        standPos = planeScript.RandomPointOutBounds();
        EventManager.ScoreCard -= OnScoreCard;
        EventManager.RandomizePreferences -= OnRandomizePreferences;
        EventManager.PresentCard -= ResetCalculatedScore;
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
            planeScript.RemoveVisitor(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!beenHit && !isLeaving && other.gameObject.tag == "ArtPiece")
        {
            Debug.Log("collision with artpiece");
            beenHit = true;
            animController.anim.SetTrigger("falloverback");

            // detracting the score if artpiece has been score already
            
            EventManager.EmitAddBaseValueToGamestate(-calculatedScore);
            scoreVisualizer.showScore(System.Math.Round(-calculatedScore), 0);
        }
    }
}

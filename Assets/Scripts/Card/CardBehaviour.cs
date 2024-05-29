using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    protected PlayingCardScript parentScript;
    public CardProps cardProps;
    [HideInInspector] public float cardValue;
    [HideInInspector] public float cardCost;
    // Start is called before the first frame update
    void Start()
    {
        parentScript = GetComponentInParent<PlayingCardScript>();
        cardValue = cardProps.baseValue;
        cardCost = cardProps.cost;
        parentScript.DisplayCardStats(cardValue, cardCost);
    }
    internal void AddCounter(params Counter[] counter) => EventManager.EmitAddCounter(counter);

    public virtual void Play()
    {
        EventManager.EmitScoreCard(this);
    }
}

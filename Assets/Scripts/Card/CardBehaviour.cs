using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    protected PlayingCardScript parentScript;
    public CardProps cardProps;
    [Header("Properties")]
    public float currentCritMult;
    public float currentCritChance;
    [HideInInspector] public float cardValue;
    [HideInInspector] public float cardCost;
    [HideInInspector] public bool guaranteedCrit;

    [HideInInspector] public List<Func<CardBehaviour, bool>> revertBuffFuncs = new();
    internal bool wasInPlayzone = false;
    internal bool buffingOtherCards = false;
    internal List<CardBehaviour> buffedMediumCards = new();
    internal List<CardBehaviour> buffedTypeCards = new();
    // Start is called before the first frame update
    void Start()
    {
        EventManager.CurationDone += ResetCardStats;
        parentScript = GetComponentInParent<PlayingCardScript>();
        ResetCardStats();
    }

    void ResetCardStats()
    {
        cardValue = cardProps.baseValue;
        cardCost = cardProps.cost;
        guaranteedCrit = false;
        currentCritMult = cardProps.critMult;
        currentCritChance = cardProps.critChance;
        DisplayCardStats();
    }

    internal void DisplayCardStats()
    {
        parentScript.DisplayCardStats(cardCost, cardValue, guaranteedCrit);
    }


    internal void AddCounter(params Counter[] counter) => EventManager.EmitAddCounter(counter);

    public virtual void Play()
    {
        //This is commented for debugging purposes, TODO: remove the comment when releasing for random crits
        if (guaranteedCrit /*|| UnityEngine.Random.value < currentCritChance*/)
        {
            cardValue *= currentCritMult;
        }
        EventManager.EmitScoreCard(this);
    }

    internal virtual bool OnAddToPlayZone()
    {
        if (wasInPlayzone) return false;
        foreach (Medium m in cardProps.medium)
        {
            foreach (Func<CardBehaviour, bool> mediumEffect in CardEffects.MediumEffects[m])
            {
                mediumEffect(this);
            }
        }

        foreach (CardType t in cardProps.cardType)
        {
            foreach (Func<CardBehaviour, bool> typeEffect in CardEffects.TypeEffects[t])
            {
                typeEffect(this);
            }
        }

        wasInPlayzone = true;
        return true;
    }

    internal virtual bool OnRemoveFromPlayZone()
    {
        if (!wasInPlayzone) return false;


        foreach (Medium m in cardProps.medium)
        {
            CardEffects.MediumEffects[m].Remove(this.MediumEffect);
        }

        foreach (CardType t in cardProps.cardType)
        {
            CardEffects.TypeEffects[t].Remove(TypeEffect);
        }

        if (buffingOtherCards)
        {
            foreach (CardBehaviour buffedCard in buffedMediumCards)
            {
                RevertMediumEffect(buffedCard);
            }
            buffedMediumCards.Clear();
            foreach (CardBehaviour buffedCard in buffedTypeCards)
            {
                RevertTypeEffect(buffedCard);
            }
            buffedTypeCards.Clear();
            buffingOtherCards = false;
        }

        foreach (Func<CardBehaviour, bool> revertFunc in revertBuffFuncs)
        {
            revertFunc(this);
        }
        revertBuffFuncs.Clear();
        wasInPlayzone = false;
        return true;
    }

    internal virtual bool MediumEffect(CardBehaviour card) { return false; }
    internal virtual bool RevertMediumEffect(CardBehaviour card) { return false; }
    internal virtual bool TypeEffect(CardBehaviour card) { return false; }
    internal virtual bool RevertTypeEffect(CardBehaviour card) { return false; }
}

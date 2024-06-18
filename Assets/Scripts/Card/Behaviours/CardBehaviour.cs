using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    [HideInInspector] public bool ignoreDislike;
    [HideInInspector] public bool leaveOnDislike;


    [HideInInspector] public List<Func<CardBehaviour, bool>> revertBuffFuncs = new();
    internal bool wasInPlayzone = false;
    internal bool buffingOtherCards = false;
    internal List<CardBehaviour> buffedMediumCards = new();
    internal List<CardBehaviour> buffedTypeCards = new();
    public int pos;

    // Start is called before the first frame update

    private void Awake()
    {
        EventManager.CurationDone += ResetCardStats;
        EventManager.AllOfMediumEffect += OnAllOfMediumEffect;
        EventManager.AllOfTypeEffect += OnAllOfTypeEffect;
        if (!cardProps.cardType.Contains(CardType.ANY)) cardProps.cardType.Add(CardType.ANY);
        InitializeCardStats();
    }

    internal virtual void Start()
    {
        parentScript = GetComponentInParent<PlayingCardScript>();
        ResetCardStats();
    }

    private void OnAllOfTypeEffect(CardType type, Func<CardBehaviour, bool> func)
    {
        if (cardProps.cardType.Contains(type))
        {
            func(this);
        }
    }

    private void OnAllOfMediumEffect(Medium medium, Func<CardBehaviour, bool> func)
    {
        if (cardProps.medium.Contains(medium))
        {
            func(this);
        }
    }

    public void ResetCardStats()
    {
        InitializeCardStats();
        DisplayCardStats();
    }

    private void InitializeCardStats()
    {
        wasInPlayzone = false;
        cardValue = cardProps.baseValue;
        cardCost = cardProps.cost;
        guaranteedCrit = false;
        ignoreDislike = false;
        leaveOnDislike = false;
        buffingOtherCards = false;
        revertBuffFuncs.Clear();
        buffedTypeCards.Clear();
        buffedMediumCards.Clear();
        currentCritMult = cardProps.critMult;
        currentCritChance = cardProps.critChance;
    }

    internal void DisplayCardStats()
    {
        parentScript.DisplayCardStats(cardCost, cardValue, guaranteedCrit);
    }

    // This is done via events, as other cards may activate an effect whenever a counter is added in the future.
    internal void AddCounter(params Counter[] counter) => EventManager.EmitAddCounter(counter);
    internal void RemoveCounter(params Counter[] counter) => EventManager.EmitRemoveCounter(counter);

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
        GameStateManager.instance.UpdateAvailableCoins(0, cardCost);

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

        // Remove the effects still lingering in the effect pool by this card from the pool
        RemoveEffectsFromEffectPool();
        // Reverts the effects already applied to other cards
        RevertEffectsOfBuffedCards();
        // Reverts all effects applied to this card
        foreach (Func<CardBehaviour, bool> revertFunc in revertBuffFuncs)
        {
            revertFunc(this);
        }
        revertBuffFuncs.Clear();

        wasInPlayzone = false;

        GameStateManager.instance.UpdateAvailableCoins(cardCost, 0);
        return true;
    }
    /// <summary>
    /// Gets called in CardSlotDeciderScript whenever two cards are being swapped.
    /// </summary>
    /// <param name="swappedWith">The CardBehaviour swapped with</param>
    /// <returns>True if the swapped card was in playzone</returns>
    internal virtual bool OnSwapCardSlot(CardBehaviour swappedWith)
    {
        return wasInPlayzone;
    }

    private void RemoveEffectsFromEffectPool()
    {
        foreach (Medium m in cardProps.medium)
        {
            CardEffects.MediumEffects[m].Remove(this.MediumEffect);
        }

        foreach (CardType t in cardProps.cardType)
        {
            CardEffects.TypeEffects[t].Remove(TypeEffect);
        }
    }

    private void RevertEffectsOfBuffedCards()
    {
        if (buffingOtherCards)
        {
            for (int i = 0; i < buffedMediumCards.Count;)
            {
                RevertMediumEffect(buffedMediumCards[i]);

            }
            for (int i = 0; i < buffedTypeCards.Count;)
            {
                RevertTypeEffect(buffedTypeCards[i]);
            }
            buffingOtherCards = false;
        }
    }

    internal virtual bool MediumEffect(CardBehaviour card) { return false; }
    internal virtual bool RevertMediumEffect(CardBehaviour card) { return ApplyRevert(card, buffedMediumCards); }
    internal virtual bool TypeEffect(CardBehaviour card) { return false; }
    internal virtual bool RevertTypeEffect(CardBehaviour card) { return ApplyRevert(card, buffedTypeCards); }
    internal bool ApplyTypeEffect(CardBehaviour cardToBuff)
    {
        buffingOtherCards = true;
        buffedTypeCards.Add(cardToBuff);
        cardToBuff.revertBuffFuncs.Add(RevertTypeEffect);
        cardToBuff.DisplayCardStats();
        return true;
    }
    internal bool ApplyMediumEffect(CardBehaviour cardToBuff)
    {
        buffingOtherCards = true;
        buffedMediumCards.Add(cardToBuff);
        cardToBuff.revertBuffFuncs.Add(RevertMediumEffect);
        cardToBuff.DisplayCardStats();
        return true;
    }
    internal bool ApplyRevert(CardBehaviour card, List<CardBehaviour> buffedCards)
    {
        buffingOtherCards = false;
        card.DisplayCardStats();
        buffedCards.Remove(card);
        return true;
    }

    internal float ApplyDiscount(CardBehaviour card, float discount)
    {
        GameStateManager.instance.UpdateAvailableCoins(card.cardCost, card.cardCost - discount);

        card.cardCost -= discount;
        return card.cardCost;
    }

    internal float RevertDiscount(CardBehaviour card, float discount)
    {
        GameStateManager.instance.UpdateAvailableCoins(card.cardCost, card.cardCost + discount);
        card.cardCost += discount;
        return card.cardCost;
    }
}

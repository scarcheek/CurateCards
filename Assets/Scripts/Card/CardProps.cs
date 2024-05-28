using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CardProps : ScriptableObject, IEquatable<CardProps>
{
    [Header("Only gameplay")]
    public GameObject presentPrefab;
    public float critMult = 2;
    public float critChance = 0.1f;
    [Header("Only UI")]
    public string title;
    public string description;
    public Sprite cardSprite;
    public int cost;
    public List<Effect> effects = new();
    [Header("Both")]
    public int baseValue;
    [HideInInspector] public float value;
    public List<CardType> cardType = new();
    public List<Medium> medium = new();

    public bool crits = false;

    private GUID uID;
    internal List<Func<CardProps, bool>> revertFuncs = new();

    [HideInInspector] public int propPos;
    [HideInInspector] public bool inPlayZone;


    private void Awake()
    {
        uID = GUID.Generate();
        EventManager.CardPutInPlayzone += OnCardPutInPlayzone;
        EventManager.CardRemoveFromPlayzone += OnCardRemoveFromPlayzone;
        EventManager.CurationDone += ResetCardState;
        ResetCardState();
    }


    private void ResetCardState()
    {
        inPlayZone = false;
        propPos = 0;
        crits = false;
        value = baseValue;
    }

    protected void AddCounter(params Counter[] counter) => EventManager.EmitAddCounter(counter);

    public virtual void Play()
    {
        if (crits /*|| UnityEngine.Random.value < critChance*/)
        {
            value = baseValue * critMult;
        }
        else
        {
            value = baseValue;
        }
        EventManager.EmitScoreCard(this);
    }

    public void SetPropPos(int pos)
    {
        propPos = pos;
    }

    internal virtual void OnCardPutInPlayzone(CardProps card)
    {
        if (Equals(card)) card.ThisCardPutInPlayzone();
        else this.OtherCardPutInPlayzone(card);
    }
    internal virtual void ThisCardPutInPlayzone()
    {
        foreach (Medium m in medium)
        {
            if (GameStateManager.instance.ActiveEffects.ContainsKey(m)
                && !GameStateManager.instance.ActiveEffects[m](this))
            {
                GameStateManager.instance.ActiveEffects.Remove(m);
            }
        }
    }
    internal virtual void OtherCardPutInPlayzone(CardProps other) { }

    internal virtual void OnCardRemoveFromPlayzone(CardProps card)
    {
        if (card == this) card.ThisCardRemovedFromPlayzone();
        else card.OtherCardRemovedFromPlayzone(this);

    }

    internal virtual void ThisCardRemovedFromPlayzone()
    {
        foreach (Func<CardProps, bool> revertFunc in revertFuncs)
        {
            revertFunc(this);
        }
    }
    internal virtual void OtherCardRemovedFromPlayzone(CardProps other) { }

    internal virtual void OnSwapPropPos(CardProps card)
    {
        GameStateManager gameStateManager = GameStateManager.instance;
        foreach (Medium m in medium)
        {
            if (gameStateManager.ActiveEffects.ContainsKey(m))
            {
                gameStateManager.ActiveEffects[m] -= SameMediumEffect;
            }
        }
    }

    internal virtual bool SameMediumEffect(CardProps card) { return false; }
    internal virtual bool RevertSameMediumEffect(CardProps card) {
        card.revertFuncs.Remove(this.RevertSameMediumEffect);
        return false;
    }
    internal virtual void SameTypeEffect(CardProps card) { }
    internal virtual void RevertSameTypeEffect(CardProps card) { }

    /// <summary>
    /// Sets crits to true, can be overritten for on-crit effects
    /// </summary>
    internal virtual void Crit()
    {
        crits = true;
    }



    public bool Equals(CardProps other)
    {
        return uID.Equals(other.uID);
    }
}


public enum Counter
{
    attack,
    defence,
    virus
}

public enum CardType
{
    traditional,
    contemporary,
    technological,
    everyday,
    ancient
}

public enum Medium
{
    sculpture,
    painting,
    furniture,
    chair,
    food,
    music,
    videogame,
    video,
    none
}
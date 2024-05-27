using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardProps : ScriptableObject
{
    [Header("Only gameplay")]
    public GameObject presentPrefab;
    [Header("Only UI")]
    public string title;
    public string description;
    public Sprite cardSprite;
    public int cost;
    public List<Effect> effects = new();
    [Header("Both")]
    public int baseValue;
    public List<CardType> cardType = new();
    public List<Medium> medium = new();


    protected void AddCounter(params Counter[] counter) => EventManager.EmitAddCounter(counter);

    public virtual void Play()
    {
        Debug.LogWarning("Play() has not been implemented for card: " + title);
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
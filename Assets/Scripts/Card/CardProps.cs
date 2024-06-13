using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardProps")]

public class CardProps : ScriptableObject
{
    [Header("Only gameplay")]
    public GameObject presentPrefab;
    public float critChance = 0.1f;
    public float critMult = 2f;
    [Header("Only UI")]
    public string title;
    public string description;
    public Sprite cardSprite;
    public int cost;
    public int shopCost;
    public List<Effect> effects = new();
    [Header("Both")]
    public int baseValue;
    public List<CardType> cardType = new();
    public List<Medium> medium = new();

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
    ancient,
    ANY
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
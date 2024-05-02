using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Card")]
public class Card : ScriptableObject
{
    [Header("Only gameplay")]
    public GameObject presentPrefab;
    [Header("Only UI")]
    public string title;
    public string description;
    public Sprite cardSprite;
    public int cost;
    [Header("Both")]
    public int baseValue;
    public CardType cardType;
    public Medium medium;
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
    food,
    music,
    videogame,
    none
}
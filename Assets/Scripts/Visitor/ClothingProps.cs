using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ClothingProp")]
public class ClothingProps : ScriptableObject
{
    public Sprite sprite;
    public ClothingType clothingType;
    [Header("Preferences")]
    [SerializeField] private float traditional = 0.0f;
    [SerializeField] private float contemporary = 0.0f;
    [SerializeField] private float technological = 0.0f;
    [SerializeField] private float everyday = 0.0f;
    [SerializeField] private float ancient = 0.0f;
    public Dictionary<CardType, float> prefernces = new Dictionary<CardType, float>();

    private void Awake()
    {
        prefernces = new Dictionary<CardType, float>() {
            {CardType.traditional, traditional},
            {CardType.contemporary, contemporary},
            {CardType.technological, technological},
            {CardType.everyday, everyday},
            {CardType.ancient, ancient}
        };
    } 
}
public enum ClothingType
{
    hat,
    pants,
    body
}
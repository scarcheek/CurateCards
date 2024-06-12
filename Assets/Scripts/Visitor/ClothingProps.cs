using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ClothingProp")]
public class ClothingProps : ScriptableObject
{
    [SerializeField] public ClothingType clothingType;
    [SerializeField] private float traditional = 0.0f;
    [SerializeField] private float contemporary = 0.0f;
    [SerializeField] private float technological = 0.0f;
    [SerializeField] private float everyday = 0.0f;
    [SerializeField] private float ancient = 0.0f;
    public Dictionary<CardType, float> prefernces = new Dictionary<CardType, float>();
    [SerializeField] public Sprite sprite;

    private void OnEnable()
    {
        Debug.Log("Enabled clothing");
        prefernces = new Dictionary<CardType, float>() {
            {CardType.traditional, traditional},
            {CardType.contemporary, contemporary},
            {CardType.technological, technological},
            {CardType.everyday, everyday},
            {CardType.ancient, ancient}
        };
    }
    private void Awake()
    {
        Debug.Log("Awake clothing");
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
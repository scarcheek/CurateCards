using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShopKeeperProp")]

public class ShopKeeperProp : ScriptableObject
{
    [Header("Meine coolen props danke")]
    [SerializeField] public GameObject BodyPart;
    public Vector3 startPos;
    public Vector3 endPos;
    public Vector3 endColor;
    
}

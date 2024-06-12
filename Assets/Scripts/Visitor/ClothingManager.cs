using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class ClothingManager : MonoBehaviour
{
    [SerializeField] ClothingProps[] hats;
    [SerializeField] ClothingProps[] bodies;
    [SerializeField] ClothingProps[] pants;


    public ClothingProps randomHat()
    {
        return hats[Random.Range(0, hats.Length)];
    }

    public ClothingProps randomBody()
    {
        return bodies[Random.Range(0, bodies.Length)];
    }

    public ClothingProps randomPants()
    {
        return pants[Random.Range(0, pants.Length)];
    }
}

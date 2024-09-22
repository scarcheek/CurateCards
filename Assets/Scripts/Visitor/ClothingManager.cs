using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class ClothingManager : MonoBehaviour
{
    [SerializeField] ClothingProps[] hats;
    [SerializeField] ClothingProps[] bodies;
    [SerializeField] ClothingProps[] pants;
    [Header("DEBUG")]
    [SerializeField] private bool randomizeClothes;

    public ClothingProps randomHat()
    {
        if (!randomizeClothes)
        {
            return Instantiate(hats[0]);
        }
        return Instantiate(hats[Random.Range(0, hats.Length)]);
    }

    public ClothingProps randomBody()
    {
        if (!randomizeClothes)
        {
            return Instantiate(bodies[0]);
        }
        return Instantiate(bodies[Random.Range(0, bodies.Length)]);
    }

    public ClothingProps randomPants()
    {
        if (!randomizeClothes)
        {
            return Instantiate(pants[0]);
        }
        return Instantiate(pants[Random.Range(0, pants.Length)]);
    }
}

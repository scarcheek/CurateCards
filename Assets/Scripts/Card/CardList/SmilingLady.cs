using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/SmilingLady")]
public class SmilingLady : CardProps
{
    public override void Play()
    {
        Debug.Log("I am the mona lisa :)");
        base.Play();

    }
}

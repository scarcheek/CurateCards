using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SmilingLady : CardBehaviour
{
    public override void Play()
    {
        Debug.Log("I am the mona lisa :)");
        base.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uhhmm : CardBehaviour
{
    public override void Play()
    {
        EventManager.EmitRandomizePreferences();
        base.Play();
    }
}

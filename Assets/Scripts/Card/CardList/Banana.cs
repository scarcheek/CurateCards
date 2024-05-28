using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Banana")]
public class Banana : CardProps
{
    private CardProps buffing;
    public override void Play()
    {
        Debug.Log("Mmmh banana :)");
        base.Play();
    }

    internal override void ThisCardPutInPlayzone()
    {
        base.ThisCardPutInPlayzone();

        if (!GameStateManager.instance.ActiveEffects.TryAdd(Medium.food, SameMediumEffect))
        {
            GameStateManager.instance.ActiveEffects[Medium.food] += SameMediumEffect;
        }
    }
    internal override void OnSwapPropPos(CardProps card)
    {
        //TODO: Figure out what to do when swapping, removing is also very not tested but
        //i cant because i havent eaten the entire day so fuck you for now, we have multiple bananas
        //that just crit for the sake of it
        base.OnSwapPropPos(card);
        if (card.propPos < propPos)
        {
            RevertSameMediumEffect(card);
            card.revertFuncs.Remove(RevertSameMediumEffect);
        }
        if (card.propPos > propPos)
        {
            SameMediumEffect(card);
            GameStateManager.instance.ActiveEffects[Medium.food] -= RevertSameMediumEffect;
        }
    }

    internal override void ThisCardRemovedFromPlayzone()
    {
        base.ThisCardRemovedFromPlayzone();
        if (buffing != null) RevertSameMediumEffect(buffing);
        if (GameStateManager.instance.ActiveEffects.ContainsKey(Medium.food))
        {
            GameStateManager.instance.ActiveEffects[Medium.food] -= SameMediumEffect;
        }
    }

    internal override bool SameMediumEffect(CardProps other)
    {
        if(other.propPos > propPos)
        {
            Debug.Log("I will now crit.." + other.propPos);
            other.Crit();
            buffing = other;
            other.revertFuncs.Add(RevertSameMediumEffect);
            return false;
        }
        return true;
    }


    internal override bool RevertSameMediumEffect(CardProps card)
    {
        base.RevertSameMediumEffect(card);
        card.crits = false;
        return true;
    }

}

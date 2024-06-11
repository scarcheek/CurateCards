using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Pineapple : CardBehaviour
{
    [SerializeField] float discount = 5;
    

    internal override bool OnAddToPlayZone()
    {
        if (!base.OnAddToPlayZone()) return false;
        CardEffects.MediumEffects[Medium.food].Add(MediumEffect);
        return true;
    }

    internal override bool MediumEffect(CardBehaviour card)
    {
        return AddAttackCounter(card);
    }

    internal override bool RevertMediumEffect(CardBehaviour card)
    {
        return RevertAddAttackCounter(card);
    }

    internal bool AddAttackCounter(CardBehaviour cardToBuff)
    {
        if (!buffedMediumCards.Contains(cardToBuff))
        {
            cardToBuff.cardCost -= discount;
            return ApplyMediumEffect(cardToBuff);
        }
        return false;
    }

    internal bool RevertAddAttackCounter(CardBehaviour card)
    {
        if (buffedMediumCards.Contains(card))
        {
            card.cardCost += discount;
            return ApplyRevert(card);
        }
        return false;
    }
}

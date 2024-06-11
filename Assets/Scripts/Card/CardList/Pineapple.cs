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
        return DiscountFoodCard(card);
    }

    internal override bool RevertMediumEffect(CardBehaviour card)
    {
        return RevertDiscountFoodCard(card);
    }

    internal bool DiscountFoodCard(CardBehaviour cardToBuff)
    {
        if (!buffedMediumCards.Contains(cardToBuff))
        {
            ApplyDiscount(cardToBuff, discount);
            return ApplyMediumEffect(cardToBuff);
        }
        return false;
    }

    internal bool RevertDiscountFoodCard(CardBehaviour card)
    {
        if (buffedMediumCards.Contains(card))
        {
            RevertDiscount(card, discount);
            return ApplyRevert(card, buffedMediumCards);
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venus : CardBehaviour
{
    [SerializeField] private float valIncrease = 5;

    internal override bool OnAddToPlayZone()
    {
        if (!base.OnAddToPlayZone()) return false;
        CardEffects.TypeEffects[CardType.ANY].Add(VenusValueIncreaseAfter);

        EventManager.EmitAllOfTypeEffect(CardType.ANY, VenusValueIncreaseBefore);
        return true;
    }

    internal override bool OnRemoveFromPlayZone()
    {
        if (!base.OnRemoveFromPlayZone()) return false;
        EventManager.EmitAllOfTypeEffect(CardType.ANY, RevertVenusValueIncrease);
        return true;
    }


    internal override bool TypeEffect(CardBehaviour card)
    {
        return VenusValueIncreaseAfter(card);
    }
    private bool VenusValueIncreaseBefore(CardBehaviour card)
    {
        if (card != this && !buffedTypeCards.Contains(card) && card.wasInPlayzone)
        {
            cardValue += valIncrease;
            DisplayCardStats();
            ApplyTypeEffect(card);
            return true;
        }
        return false;
    }
    private bool VenusValueIncreaseAfter(CardBehaviour card)
    {
        if (card != this && !card.wasInPlayzone)
        {
            cardValue += valIncrease;
            DisplayCardStats();
            ApplyTypeEffect(card);
            return true;
        }
        return false;
    }

    internal override bool RevertTypeEffect(CardBehaviour card)
    {
        return RevertVenusValueIncrease(card);
    }
    private bool RevertVenusValueIncrease(CardBehaviour card)
    {
        if (buffedTypeCards.Contains(card))
        {
            cardValue -= valIncrease;
            DisplayCardStats();
            return ApplyRevert(card);
        }
        return false;
    }
}

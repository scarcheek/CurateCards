using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : CardBehaviour
{
    [SerializeField] private float valIncrease;

    internal override bool OnAddToPlayZone()
    {
        if (!base.OnAddToPlayZone()) return false;
        EventManager.EmitAllOfMediumEffect(Medium.chair, ChairValIncrease);
        return true;
    }

    internal override bool MediumEffect(CardBehaviour card)
    {
        return ChairValIncrease(card);

    }
    internal override bool RevertMediumEffect(CardBehaviour card)
    {
        return RevertChairValIncrease(card);
    }

    internal bool ChairValIncrease(CardBehaviour cardToBuff)
    {
        if (cardToBuff != this && !buffedTypeCards.Contains(cardToBuff) && cardToBuff.cardProps.medium.Contains(Medium.chair))
        {
            cardToBuff.cardValue += valIncrease;
            return ApplyMediumEffect(buffedTypeCards, cardToBuff);
        }
        return false;
    }

    internal bool RevertChairValIncrease(CardBehaviour card)
    {
        if (card != this && buffedTypeCards.Contains(card))
        {
            card.cardCost -= valIncrease;
            return ApplyRevert(card);
        }
        return false;
    }
}

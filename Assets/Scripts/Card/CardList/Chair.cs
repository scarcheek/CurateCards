using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : AllOfBuffCard
{
    [SerializeField] private float valIncrease;


    internal override bool OnAddToPlayZone()
    {
        if (!base.OnAddToPlayZone()) return false;
        EventManager.EmitAllOfMediumEffect(Medium.chair, ChairValIncrease);
        return true;
    }

    internal override bool OnRemoveFromPlayZone()
    {
        if (!base.OnRemoveFromPlayZone()) return false;
        EventManager.EmitAllOfMediumEffect(Medium.chair, RevertChairValIncrease);
        return true;
    }


    internal bool ChairValIncrease(CardBehaviour cardToBuff)
    {
        if (ApplyCondition(cardToBuff, Medium.chair))
        {
            cardToBuff.cardValue += valIncrease;
            return ApplyAllOfEffect(cardToBuff);
        }
        return false;
    }

    internal bool RevertChairValIncrease(CardBehaviour card)
    {
        if (card != this && buffedCards.Contains(card))
        {
            card.cardValue -= valIncrease;
            return RevertAllOfEffect(card);
        }
        return false;
    }
}

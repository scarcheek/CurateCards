using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongPause : CardBehaviour
{
    float appliedDiscount = 0f;

    internal override bool OnAddToPlayZone()
    {
        if(!base.OnAddToPlayZone()) return false;
        CardEffects.TypeEffects[CardType.ANY].Add(TypeEffect);
        return true;
    }
    internal override bool TypeEffect(CardBehaviour card)
    {
        return NextCardFree(card);
    }

    internal override bool RevertTypeEffect(CardBehaviour card)
    {
        return RevertNextCardFree(card);
    }

    internal bool NextCardFree(CardBehaviour cardToBuff)
    {
        if (!buffingOtherCards)
        {
            appliedDiscount = cardToBuff.cardCost;
            ApplyDiscount(cardToBuff, appliedDiscount);
            return ApplyTypeEffect(cardToBuff);
        }
        return false;
    }

    internal bool RevertNextCardFree(CardBehaviour card)
    {
        if (buffedTypeCards.Contains(card))
        {
            RevertDiscount(card, appliedDiscount);
            appliedDiscount = 0;
            return ApplyRevert(card, buffedTypeCards);
        }
        return false;
    }
}

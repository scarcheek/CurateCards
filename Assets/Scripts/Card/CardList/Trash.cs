using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : CardBehaviour
{
    [SerializeField] float valueFactor = 2;
    private float valueBeforeChange = 0;
    internal override bool OnAddToPlayZone()
    {
        if (!base.OnAddToPlayZone()) return false;
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
            valueBeforeChange = cardToBuff.cardValue;
            cardToBuff.cardValue *= valueFactor;

            return ApplyTypeEffect(cardToBuff);
        }
        return false;
    }

    internal bool RevertNextCardFree(CardBehaviour card)
    {
        if (buffedTypeCards.Contains(card))
        {
            card.cardValue = valueBeforeChange;
            return ApplyRevert(card);
        }
        return false;
    }
}

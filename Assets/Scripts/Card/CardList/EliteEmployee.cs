using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEmployee : CardBehaviour
{
    internal override bool OnAddToPlayZone()
    {
        if (!base.OnAddToPlayZone()) return false;
        CardEffects.TypeEffects[CardType.ANY].Add(TypeEffect);
        return true;
    }

    internal override bool TypeEffect(CardBehaviour card)
    {
        return NextCardCrit(card);
    }
    internal override bool RevertTypeEffect(CardBehaviour card)
    {
        return RevertCardCrit(card);
    }

    internal bool NextCardCrit(CardBehaviour cardToBuff)
    {
        if (!buffingOtherCards && cardToBuff.cardProps.cardType.Contains(CardType.ANY))
        {
            cardToBuff.guaranteedCrit = true;

            return ApplyTypeEffect(cardToBuff);
        }
        return false;
    }

    internal bool RevertCardCrit(CardBehaviour card)
    {
        if (buffedTypeCards.Contains(card))
        {
            card.guaranteedCrit = false;
            return ApplyRevert(card);
        }
        return false;
    }
}

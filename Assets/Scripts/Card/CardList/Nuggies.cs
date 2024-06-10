using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuggies : CardBehaviour
{
    internal override bool OnAddToPlayZone()
    {
        if(!base.OnAddToPlayZone()) return false;
        CardEffects.TypeEffects[CardType.ANY].Add(TypeEffect);
        return true;
    }

    internal override bool TypeEffect(CardBehaviour card)
    {
        return NextIgnoreDislike(card);
    }

    internal override bool RevertTypeEffect(CardBehaviour card)
    {
        return RevertNextIgnoreDislike(card);
    }

    internal bool NextIgnoreDislike(CardBehaviour cardToBuff)
    {
        if (!buffingOtherCards)
        {
            Debug.Log(cardToBuff.name + " will now ignore Dislikes");
            cardToBuff.ignoreDislike = true;

            return ApplyTypeEffect(cardToBuff);
        }
        return false;
    }

    internal bool RevertNextIgnoreDislike(CardBehaviour card)
    {
        if (buffedTypeCards.Contains(card))
        {
            Debug.Log(card.name + " will now apply Dislikes");
            card.ignoreDislike = false;
            return ApplyRevert(card);
        }
        return false;
    }
}

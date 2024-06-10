using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckedCat : AllOfBuffCard
{
    [SerializeField] private float discount;

    internal override bool OnAddToPlayZone()
    {
        if (!base.OnAddToPlayZone()) return false;
        EventManager.EmitAllOfTypeEffect(CardType.ancient, AncientCostReduce);
        return true;
    }

    internal override bool OnRemoveFromPlayZone()
    {
        if (!base.OnRemoveFromPlayZone()) return false;
        EventManager.EmitAllOfTypeEffect(CardType.ancient, RevertAncientCostReduce);
        return true;
    }

    internal bool AncientCostReduce(CardBehaviour cardToBuff)
    {
        if (ApplyCondition(cardToBuff, CardType.ancient))
        {
            cardToBuff.cardCost -= discount;
            return ApplyTypeEffect(cardToBuff);
        }
        return false;
    }

    internal bool RevertAncientCostReduce(CardBehaviour card)
    {
        if (card != this && buffedTypeCards.Contains(card))
        {
            card.cardCost += discount;
            return ApplyRevert(card);
        }
        return false;
    }
}

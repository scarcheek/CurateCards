using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckedCat : CardBehaviour
{
    [SerializeField] private float discount;

    internal override bool OnAddToPlayZone()
    {
        if (!base.OnAddToPlayZone()) return false;
        EventManager.EmitAllOfTypeEffect(CardType.ancient, AncientCostReduce);
        return true;
    }


    internal override bool TypeEffect(CardBehaviour card)
    {
        return AncientCostReduce(card);
    }
    internal override bool RevertTypeEffect(CardBehaviour card)
    {
        return RevertAncientCostReduce(card);
    }

    internal bool AncientCostReduce(CardBehaviour cardToBuff)
    {
        if (cardToBuff != this && !buffedTypeCards.Contains(cardToBuff) && cardToBuff.cardProps.cardType.Contains(CardType.ancient))
        {
            cardToBuff.cardCost -= discount;
            return ApplyEffect(buffedTypeCards, cardToBuff);
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

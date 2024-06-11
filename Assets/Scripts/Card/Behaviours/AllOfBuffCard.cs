using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllOfBuffCard : CardBehaviour
{
    internal List<CardBehaviour> buffedCards = new();

    internal bool ApplyAllOfEffect(CardBehaviour cardToBuff)
    {
        buffingOtherCards = true;
        cardToBuff.DisplayCardStats();
        buffedCards.Add(cardToBuff);
        return true;
    }

    internal bool RevertAllOfEffect(CardBehaviour card)
    {
        return ApplyRevert(card, buffedCards);
    }

    internal bool ApplyCondition(CardBehaviour card, Medium m)
    {
        return card != this && !buffedCards.Contains(card) && card.cardProps.medium.Contains(m);
    }

    internal bool ApplyCondition(CardBehaviour card, CardType t)
    {
        return card != this && !buffedCards.Contains(card) && card.cardProps.cardType.Contains(t);

    }
}

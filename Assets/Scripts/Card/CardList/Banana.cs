using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : CardBehaviour
{
    internal override bool OnAddToPlayZone()
    {
        if (!base.OnAddToPlayZone()) return false;
        CardEffects.MediumEffects[Medium.food].Add(MediumEffect);
        return true;
    }

    internal override bool MediumEffect(CardBehaviour card)
    {
        return NextFoodCrit(card);
    }
    internal override bool RevertMediumEffect(CardBehaviour card)
    {
        return RevertFoodCrit(card);
    }

    internal bool NextFoodCrit(CardBehaviour cardToBuff)
    {
        if (!buffingOtherCards && cardToBuff.cardProps.medium.Contains(Medium.food))
        {
            Debug.Log(cardToBuff.name + " will now crit");
            buffingOtherCards = true;
            buffedMediumCards.Add(cardToBuff);
            cardToBuff.guaranteedCrit = true;
            cardToBuff.revertBuffFuncs.Add(RevertMediumEffect);
            cardToBuff.DisplayCardStats();
            return true;
        }
        return false;
    }

    internal bool RevertFoodCrit(CardBehaviour card)
    {
        if (buffedMediumCards.Contains(card))
        {
            Debug.Log(card.name + " will now stop critting");

            buffingOtherCards = false;
            card.guaranteedCrit = false;

            card.DisplayCardStats();
            return true;
        }
        return false;
    }
}

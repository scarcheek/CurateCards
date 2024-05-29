using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : CardBehaviour
{
    internal override bool OnAddToPlayZone(List<GameObject> cardSlots)
    {
        if (!base.OnAddToPlayZone(cardSlots)) return false;
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

    internal override bool OnSwapCardSlot(CardBehaviour swappedWith)
    {
        Debug.Log("Swapped with " + swappedWith.name);
        if (!base.OnSwapCardSlot(swappedWith)) return false;
        if (buffingOtherCards)
        {
            if (buffedMediumCards.Contains(swappedWith))
            {
                RevertMediumEffect(swappedWith);
                buffedMediumCards.Remove(swappedWith);
            } 
            else
            {
                RevertMediumEffect(buffedMediumCards[0]);
                buffedMediumCards[0].revertBuffFuncs.Remove(RevertMediumEffect);
            }
        }
        else if (!buffingOtherCards && swappedWith.cardProps.medium.Contains(Medium.food))
        {
            MediumEffect(swappedWith);
        }
        if (!buffingOtherCards && pos + 1 < cardSlots.Count)
        {
            for (int i = pos + 1; i < cardSlots.Count; i++)
            {
                CardBehaviour behaviour = cardSlots[i].GetComponentInChildren<CardBehaviour>();
                if (MediumEffect(behaviour)) return true;
            }
        }

        return true;
    }
}

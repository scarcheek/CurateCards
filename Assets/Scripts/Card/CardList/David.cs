using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class David : CardBehaviour
{
    [SerializeField] private float valIncrease = 10;

    internal override bool OnAddToPlayZone()
    {
        if (!base.OnAddToPlayZone()) return false;
        CardEffects.MediumEffects[Medium.sculpture].Add(DavidValueIncreaseAfter);

        EventManager.EmitAllOfMediumEffect(Medium.sculpture, DavidValueIncreaseBefore);
        return true;
    }

    internal override bool OnRemoveFromPlayZone()
    {
        if (!base.OnRemoveFromPlayZone()) return false;
        EventManager.EmitAllOfMediumEffect(Medium.sculpture, RevertDavidValueIncrease);
        return true;
    }


    internal override bool MediumEffect(CardBehaviour card)
    {
        return DavidValueIncreaseAfter(card);
    }
    private bool DavidValueIncreaseBefore(CardBehaviour card)
    {
        if (card != this && !buffedMediumCards.Contains(card) && card.wasInPlayzone)
        {
            cardValue += valIncrease;
            DisplayCardStats();
            ApplyMediumEffect(card);
            return true;
        }
        return false;
    }
    private bool DavidValueIncreaseAfter(CardBehaviour card)
    {
        if (card != this && !card.wasInPlayzone)
        {
            cardValue += valIncrease;
            DisplayCardStats();
            ApplyMediumEffect(card);
            return true;
        }
        return false;
    }

    internal override bool RevertMediumEffect(CardBehaviour card)
    {
        return RevertDavidValueIncrease(card);
    }
    private bool RevertDavidValueIncrease(CardBehaviour card)
    {
        if (buffedMediumCards.Contains(card))
        {
            cardValue -= valIncrease;
            DisplayCardStats();
            return ApplyRevert(card);
        }
        return false;
    }

}

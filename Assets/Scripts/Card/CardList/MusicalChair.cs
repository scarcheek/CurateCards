using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicalChair : CardBehaviour
{
    private List<Counter> counters = new();

    public override void Play()
    {
        AddCounter(counters.ToArray());
        base.Play();
        counters.Clear();
    }
    internal override bool OnAddToPlayZone()
    {
        if (!base.OnAddToPlayZone()) return false;
        CardEffects.MediumEffects[Medium.chair].Add(MediumEffect);
        return true;
    }

    internal override bool MediumEffect(CardBehaviour card)
    {
        return AddAttackCounter(card);
    }

    internal override bool RevertMediumEffect(CardBehaviour card)
    {
        return RevertAddAttackCounter(card);
    }

    internal bool AddAttackCounter(CardBehaviour cardToBuff)
    {
        if (!buffedMediumCards.Contains(cardToBuff))
        {
            counters.Add(Counter.attack);
            return ApplyMediumEffect(cardToBuff);
        }
        return false;
    }

    internal bool RevertAddAttackCounter(CardBehaviour card)
    {
        if (card != this && buffedMediumCards.Contains(card))
        {
            counters.Remove(Counter.attack);
            return ApplyRevert(card);
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardEffectColors
{
    public static Dictionary<Effect, string> effectColors = new Dictionary<Effect, string>()
    {
        { Effect.addPeople, "<color=\"blue\">" },
        { Effect.addAttack, "<color=\"orange\">" },
        { Effect.addDefence, "<color=\"grey\">" },
        { Effect.addVirus, "<color=\"green\">" },
        { Effect.removeAllPeople, "<color=\"red\">" },
        { Effect.randomizePrefs, "<color=\"purple\">" },
        { Effect.crits, "<color=\"red\">" },
        { Effect.discount, "<color=\"yellow\">" },
        { Effect.addBaseVal, "<color=\"grey\">" },
        { Effect.ignoreDislike, "<color=\"green\">" },
        { Effect.ENDSTYLE, "</color>" }
    };

}

public enum Effect
{
    addPeople,
    addAttack,
    addDefence,
    addVirus,
    removeAllPeople,
    randomizePrefs,
    crits,
    discount,
    addBaseVal,
    ignoreDislike,
    ENDSTYLE
}
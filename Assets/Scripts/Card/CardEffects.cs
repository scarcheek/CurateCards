using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardEffects
{
    public static Dictionary<Medium, List<Func<CardBehaviour, bool>>> MediumEffects = new()
    {
        { Medium.chair, new()},
        { Medium.food, new()},
        { Medium.furniture, new()},
        { Medium.music, new()},
        { Medium.none, new()},
        { Medium.painting, new()},
        { Medium.sculpture, new()},
        { Medium.video, new()},
        { Medium.videogame, new()},
    };
    public static Dictionary<CardType, List<Func<CardBehaviour, bool>>> TypeEffects = new()
    {
        { CardType.ancient, new()},
        { CardType.contemporary, new()},
        { CardType.everyday, new()},
        { CardType.technological, new()},
        { CardType.traditional, new()},
        { CardType.ANY, new()},
    };

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

    public static void ClearEffects()
    {
        TypeEffects = new()
        {
            { CardType.ancient, new()},
            { CardType.contemporary, new()},
            { CardType.everyday, new()},
            { CardType.technological, new()},
            { CardType.traditional, new()},
            { CardType.ANY, new()},
        };
        MediumEffects = new()
        {
            { Medium.chair, new()},
            { Medium.food, new()},
            { Medium.furniture, new()},
            { Medium.music, new()},
            { Medium.none, new()},
            { Medium.painting, new()},
            { Medium.sculpture, new()},
            { Medium.video, new()},
            { Medium.videogame, new()},
        };
    }
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
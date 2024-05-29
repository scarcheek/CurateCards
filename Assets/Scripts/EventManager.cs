using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<GameObject> DropCardInPlayZone;
    public static event Action<GameObject> DropCardOutsidePlayZone;
    public static event Action<GameObject> OnSwapComplete;
    public static event Action<List<PlayingCardScript>> submitCards;
    public static event Action<PlayingCardScript> PresentCard;
    public static event Action AnimationVisitorDone;
    public static event Action<Counter[]> AddCounter;
    public static event Action<float> AddBaseValueToGamestate;
    public static event Action<CardBehaviour> ScoreCard;
    public static event Action CurationDone;

    public static void EmitDropCardInPlayZone(GameObject cardSlot) => DropCardInPlayZone?.Invoke(cardSlot);
    public static void EmitDropCardOutsidePlayZone(GameObject cardSlot) => DropCardOutsidePlayZone?.Invoke(cardSlot);
    public static void EmitOnSwapComplete(GameObject cardSlot) => OnSwapComplete?.Invoke(cardSlot);
    public static void EmitSubmitCards(List<PlayingCardScript> cards) => submitCards?.Invoke(cards);
    public static void EmitPresentCard(PlayingCardScript card) => PresentCard?.Invoke(card);
    public static void EmitAnimationVisitorDone() => AnimationVisitorDone?.Invoke();
    public static void EmitAddCounter(params Counter[] counter) => AddCounter?.Invoke(counter);
    public static void EmitScoreCard(CardBehaviour card) => ScoreCard?.Invoke(card);
    public static void EmitAddBaseValueToGamestate(float baseValue) => AddBaseValueToGamestate?.Invoke(baseValue);
    public static void EmitCurationDone() => CurationDone?.Invoke();
}

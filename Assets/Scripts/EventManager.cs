using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<GameObject> DropCardInPlayZone;
    public static event Action<GameObject> DropCardOutsidePlayZone;
    public static event Action<GameObject> SortSlots;
    public static event Action<List<PlayingCardScript>> submitCards;
    public static event Action<PlayingCardScript> PresentCard;
    public static event Action AnimationVisitorDone;
    public static event Action<Counter[]> AddCounter;
    public static event Action<float> AddValueToGamestate;
    public static event Action<CardProps> ScoreCard;
    public static event Action<CardProps> CardPutInPlayzone;
    public static event Action<CardProps> CardRemoveFromPlayzone;
    public static event Action CurationDone;

    public static void EmitDropCardInPlayZone(GameObject cardSlot) => DropCardInPlayZone?.Invoke(cardSlot);
    public static void EmitDropCardOutsidePlayZone(GameObject cardSlot) => DropCardOutsidePlayZone?.Invoke(cardSlot);
    public static void EmitSortSlots(GameObject cardSlot) => SortSlots?.Invoke(cardSlot);
    public static void EmitSubmitCards(List<PlayingCardScript> cards) => submitCards?.Invoke(cards);
    public static void EmitPresentCard(PlayingCardScript card) => PresentCard?.Invoke(card);
    public static void EmitAnimationVisitorDone() => AnimationVisitorDone?.Invoke();
    public static void EmitAddCounter(params Counter[] counter) => AddCounter?.Invoke(counter);
    public static void EmitScoreCard(CardProps card) => ScoreCard?.Invoke(card);
    public static void EmitCardPutInPlayzone(CardProps card) => CardPutInPlayzone?.Invoke(card);
    public static void EmitCardRemoveFromPlayzone(CardProps card) => CardRemoveFromPlayzone?.Invoke(card);

    public static void EmitAddValueToGamestate(float value) => AddValueToGamestate?.Invoke(value);
    public static void EmitCurationDone() => CurationDone?.Invoke();
}

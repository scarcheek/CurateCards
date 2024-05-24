using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<GameObject> DropCardInPlayZone;
    public static event Action<GameObject> DropCardOutsidePlayZone;
    public static event Action<GameObject> SortSlots;
    public static event Action<List<PlayingCardScript>> PlayCards;
    public static event Action<PlayingCardScript> PresentCard;
    public static event Action AnimationVisitorDone;
    public static event Action<Counter[]> AddCounter;

    public static void EmitDropCardInPlayZone(GameObject cardSlot) => DropCardInPlayZone?.Invoke(cardSlot);
    public static void EmitDropCardOutsidePlayZone(GameObject cardSlot) => DropCardOutsidePlayZone?.Invoke(cardSlot);
    public static void EmitSortSlots(GameObject cardSlot) => SortSlots?.Invoke(cardSlot);
    public static void EmitPlayCards(List<PlayingCardScript> cards) => PlayCards?.Invoke(cards);
    public static void EmitPresentCard(PlayingCardScript card) => PresentCard?.Invoke(card);
    public static void EmitAnimationVisitorDone() => AnimationVisitorDone?.Invoke();
    public static void EmitAddCounter(params Counter[] counter) => AddCounter?.Invoke(counter);
}

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
    public static event Action<Counter[]> RemoveCounter;
    public static event Action<float> AddBaseValueToGamestate;
    public static event Action<CardBehaviour> ScoreCard;
    public static event Action CurationDone;
    public static event Action RandomizePreferences;
    public static event Action CelebrateDayComplete;
    public static event Action<string> RunFailed;
    public static event Action StartTurn;
    public static event Action StartDay;
    public static event Action CheckThrow;
    public static event Action StartShopping;
    public static event Action<CardType, Func<CardBehaviour, bool>> AllOfTypeEffect;
    public static event Action<Medium, Func<CardBehaviour, bool>> AllOfMediumEffect;

    public static void EmitDropCardInPlayZone(GameObject cardSlot) => DropCardInPlayZone?.Invoke(cardSlot);
    public static void EmitDropCardOutsidePlayZone(GameObject cardSlot) => DropCardOutsidePlayZone?.Invoke(cardSlot);
    public static void EmitOnSwapComplete(GameObject cardSlot) => OnSwapComplete?.Invoke(cardSlot);
    public static void EmitSubmitCards(List<PlayingCardScript> cards) => submitCards?.Invoke(cards);
    public static void EmitPresentCard(PlayingCardScript card) => PresentCard?.Invoke(card);
    public static void EmitAnimationVisitorDone() => AnimationVisitorDone?.Invoke();
    public static void EmitAddCounter(params Counter[] counter) => AddCounter?.Invoke(counter);
    public static void EmitRemoveCounter(params Counter[] counter) => RemoveCounter?.Invoke(counter);

    public static void EmitScoreCard(CardBehaviour card) => ScoreCard?.Invoke(card);
    public static void EmitAddBaseValueToGamestate(float baseValue) => AddBaseValueToGamestate?.Invoke(baseValue);
    public static void EmitCurationDone() => CurationDone?.Invoke();
    public static void EmitRandomizePreferences() => RandomizePreferences?.Invoke();
    public static void EmitAllOfTypeEffect(CardType type, Func<CardBehaviour, bool> effect) => AllOfTypeEffect?.Invoke(type, effect);
    public static void EmitAllOfMediumEffect(Medium medium, Func<CardBehaviour, bool> effect) => AllOfMediumEffect?.Invoke(medium, effect);
    public static void EmitCelebrateDayComplete() => CelebrateDayComplete?.Invoke();
    public static void EmitRunFailed(string reason) => RunFailed?.Invoke(reason);
    public static void EmitStartShopping() => StartShopping?.Invoke();
    public static void EmitStartTurn() => StartTurn?.Invoke();
    /// <summary>
    /// Is only used when the shop closes and when GameStateManager gets loaded
    /// </summary>
    public static void EmitStartDay() => StartDay?.Invoke();
    public static void EmitCheckThrow() => CheckThrow?.Invoke();

}

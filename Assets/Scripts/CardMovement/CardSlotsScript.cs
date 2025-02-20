using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CardSlotsScript : MonoBehaviour
{
    [SerializeField] private int DebugStartCardSlots = 4;
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private Vector2 targetPos;

    [HideInInspector] public List<GameObject> cardSlots = new();
    float lowestCost = float.MinValue;
    private void Awake()
    {
        EventManager.DropCardInPlayZone += RemoveCard;
        EventManager.DropCardOutsidePlayZone += AddCardSlot;
        EventManager.OnSwapComplete += SortCardSlots;
        EventManager.submitCards += OnSubmitCards;
        EventManager.CurationDone += OnCurationDone;
        EventManager.StartTurn += PopulateHand;
    }

    private void PopulateHand()
    {
        if (DeckManager.instance.DeckList.Count == 0)
        {
            EventManager.EmitRunFailed("There are no cards left in the deck! :(");
            return;
        }
        for (int i = 0; i < DebugStartCardSlots; i++)
        {
            DrawCard();
        }

        if (lowestCost > GameStateManager.AvailableCoins) 
            EventManager.EmitRunFailed("You can't afford running another curation...");
    }

    private void FixedUpdate()
    {
        CardSlotsManager.moveToAndRecalculateTargetPos(ref targetPos, transform, cardSlots.Count);
        scrollOffset = CardSlotsManager.moveToAndRecalculateTargetPos(ref targetPos, transform, cardSlots.Count, scrollOffset);

    }

    private void Update()
    {
        if (Input.GetKeyDown(ConfigManagerScript.instance.DrawCard)) ManualDrawCard();
    }

    private void ManualDrawCard()
    {
        if (DeckManager.instance.remainingCards.Count > 0)
        {
            GameStateManager.AvailableCoins -= DeckManager.instance.CardDrawCost;

            if (lowestCost > GameStateManager.AvailableCoins)
            {
                EventManager.EmitRunFailed("That was one card too much...");
                return;
            }

            DrawCard();
        }
        else
        {
            GameStateManager.AvailableCoins -= DeckManager.instance.DeckRefreshCost;

            DeckManager.RepopulateRemainingCards();
            if (GameStateManager.AvailableCoins <= 0)
            {
                EventManager.EmitRunFailed("You couldnt afford that reshuffle...");
                return;
            }
        }
    }

    public void SetScrollOffset(float offset)
    {
        scrollOffset += offset;
    }

    private void DrawCard()
    {
        //TODO: Do card handling
        CreateAndAddNewCardSlot();
    }

    private void CreateAndAddNewCardSlot()
    {
        List<CardBehaviour> remainingCards = DeckManager.instance.remainingCards;
        if (remainingCards.Count == 0) return;

        GameObject cardSlot = Instantiate(cardSlotPrefab, transform);

        int cardIndex = Random.Range(0, remainingCards.Count);
        PlayingCardScript cardScript = cardSlot.GetComponentInChildren<PlayingCardScript>();
        CardBehaviour cardBehaviour = Instantiate(remainingCards[cardIndex], cardScript.transform);
        remainingCards.RemoveAt(cardIndex);

        lowestCost = lowestCost > cardBehaviour.cardProps.cost ? cardBehaviour.cardProps.cost : lowestCost;

        cardScript.card = cardBehaviour;
        AddCardSlot(cardSlot);
    }
    private void AddCardSlot(GameObject cardSlot) => CardSlotsManager.AddCardToPlayCardSlots(cardSlot, cardSlots, transform, cardSlotPrefab.name);
    private void RemoveCard(GameObject cardSlot) => CardSlotsManager.RemoveCardFromCardSlots(cardSlot, cardSlots, cardSlotPrefab.name);
    private void SortCardSlots(GameObject initiator = null) => CardSlotsManager.SortCardSlots(cardSlots, initiator);
    private void OnCurationDone()
    {
        CardEffects.ClearEffects();
    }
    private void OnSubmitCards(List<PlayingCardScript> cards)
    {
        CardSlotsManager.ClearCardSlots(cardSlots);
        DeckManager.RepopulateRemainingCards();
    }

    private void OnDestroy()
    {
        EventManager.DropCardInPlayZone -= RemoveCard;
        EventManager.DropCardOutsidePlayZone -= AddCardSlot;
        EventManager.OnSwapComplete -= SortCardSlots;
        EventManager.submitCards -= OnSubmitCards;
        EventManager.CurationDone -= OnCurationDone;
        EventManager.StartTurn -= PopulateHand;
    }
}
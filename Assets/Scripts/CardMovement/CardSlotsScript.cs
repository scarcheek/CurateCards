using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CardSlotsScript : MonoBehaviour
{
    [SerializeField] private GameObject cardSlotPrefab;
    [Header("DEBUG")]
    [SerializeField] private int DebugStartCardSlots = 4;
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private float scrollOffset = 0;

    [HideInInspector] public List<GameObject> cardSlots = new();
    float lowestCost = float.MaxValue;
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
        scrollOffset = CardSlotsManager.moveToAndRecalculateTargetPos(ref targetPos, transform, cardSlots.Count, scrollOffset);

    }

    private void Update()
    {
        //TODO: Make this function substract some money
        if (Input.GetKeyDown(ConfigManagerScript.instance.DrawCard)) DrawCard();
    }

    public void SetScrollOffset(float offset)
    {
        scrollOffset += offset;
    }

    private void DrawCard()
    {
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
}
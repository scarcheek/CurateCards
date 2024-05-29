using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CardSlotsScript : MonoBehaviour
{
    [SerializeField] public List<CardBehaviour> cards = new();
    [SerializeField] private int DebugStartCardSlots = 4;
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private Vector2 targetPos;

    [HideInInspector] public List<GameObject> cardSlots = new();
    private List<CardBehaviour> remainingCards = new();

    void Start()
    {
        EventManager.DropCardInPlayZone += RemoveCard;
        EventManager.DropCardOutsidePlayZone += AddCardSlot;
        EventManager.OnSwapComplete += SortCardSlots;
        EventManager.submitCards += OnSubmitCards;
        EventManager.CurationDone += OnCurationDone;

        remainingCards = cards.ToList();

        for (int i = 0; i < DebugStartCardSlots; i++)
        {
            CreateAndAddNewCardSlot();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard();
        }
    }

    private void FixedUpdate()
    {
        CardSlotsManager.moveToAndRecalculateTargetPos(ref targetPos, transform, cardSlots.Count);
    }

    private void DrawCard()
    {
        //TODO: Do card handling
        CreateAndAddNewCardSlot();
    }

    private void CreateAndAddNewCardSlot()
    {
        if (remainingCards.Count == 0) return;

        GameObject cardSlot = Instantiate(cardSlotPrefab, transform);

        int cardIndex = Random.Range(0, remainingCards.Count);
        PlayingCardScript cardScript = cardSlot.GetComponentInChildren<PlayingCardScript>();
        CardBehaviour cardBehaviour = Instantiate(remainingCards[cardIndex], cardScript.transform);
        remainingCards.RemoveAt(cardIndex);

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
        remainingCards = this.cards.ToList();
    }
}
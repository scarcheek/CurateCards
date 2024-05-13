using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class CardSlotsScript : MonoBehaviour
{
    [SerializeField] public List<CardProps> cards;
    [SerializeField] private int DebugStartCardSlots = 4;
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private Vector2 targetPos;

    [HideInInspector] public List<GameObject> cardSlots = new();
    private List<CardProps> remainingCards = new();

    void Start()
    {
        EventManager.DropCardInPlayZone += RemoveCard;
        EventManager.DropCardOutsidePlayZone += AddCardSlot;
        EventManager.SortSlots += SortCardSlots;
        EventManager.PlayCards += OnPlayCards;

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
        CardProps cardProp = remainingCards[cardIndex];
        remainingCards.RemoveAt(cardIndex);

        cardSlot.GetComponentInChildren<PlayingCardScript>().card = cardProp;

        AddCardSlot(cardSlot);
    }
    private void AddCardSlot(GameObject cardSlot) => CardSlotsManager.AddCardToPlayCardSlots(cardSlot, cardSlots, transform, cardSlotPrefab.name);
    private void RemoveCard(GameObject cardSlot) => CardSlotsManager.RemoveCardFromCardSlots(cardSlot, cardSlots, cardSlotPrefab.name);
    private void SortCardSlots(GameObject initiator = null) => CardSlotsManager.SortCardSlots(cardSlots, initiator);

    private void OnPlayCards(List<PlayingCardScript> cards)
    {
        CardSlotsManager.ClearCardSlots(cardSlots);
        remainingCards = this.cards.ToList();
    }
}
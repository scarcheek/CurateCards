using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayZoneScript : MonoBehaviour
{
    [SerializeField] private float targetSpaceBetweenCards;
    [SerializeField] private float handReAdjustmentSpeed;
    [SerializeField] private Vector2 targetPos;
    public Animator animator;
    [SerializeField] private string playZoneSlotName = "PlaySlot";
    [SerializeField] private GameObject button;
    private List<GameObject> cardSlots = new();

    [SerializeField] private float scrollOffset = 0f;


    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(false);
        targetPos = transform.localPosition;
        EventManager.DropCardInPlayZone += AddCardToPlayZone;
        EventManager.DropCardOutsidePlayZone += RemoveCardFromPlayZone;
        EventManager.OnSwapComplete += SortCardSlots;
    }

    private void FixedUpdate()
    {
        scrollOffset = CardSlotsManager.moveToAndRecalculateTargetPos(ref targetPos, transform, cardSlots.Count, scrollOffset);
    }

    public void AddCardToPlayZone(GameObject cardSlot)
    {
        CardSlotsManager.AddCardToPlayCardSlots(cardSlot, cardSlots, transform, playZoneSlotName);


        animator.SetBool("CardInPlayZone", true);

        cardSlot.GetComponentInChildren<CardSlotDeciderScript>().ResetPosition();
        cardSlot.GetComponentInChildren<CardBehaviour>().OnAddToPlayZone();

        button.SetActive(GameStateManager.AvailableCoins >= 0);
    
    }
    public void RemoveCardFromPlayZone(GameObject cardSlot)
    {
        CardSlotsManager.RemoveCardFromCardSlots(cardSlot, cardSlots, playZoneSlotName);

        animator.SetBool("CardInPlayZone", cardSlots.Count > 0);

        cardSlot.GetComponentInChildren<CardBehaviour>().OnRemoveFromPlayZone();

        button.SetActive(GameStateManager.AvailableCoins >= 0);
        if (cardSlots.Count == 0)
        {
            button.SetActive(false);
        }
    }
    public void SortCardSlots(GameObject initiator = null)
    {
        //TODO: in a perfect world men like me would not exist... but this is not a perfect world...
        // If i ever find the spark of hope in my small brain i will refactor this so next card
        // effects will work without having to re-calculate every played card.
        // But for now... this shall be the solution 
        if (CardSlotsManager.SortCardSlots(cardSlots, initiator))
        {
            CardEffects.ClearEffects();
            cardSlots.ForEach(slot =>
            {
                CardBehaviour behaviour = slot.GetComponentInChildren<CardBehaviour>();
                behaviour.OnRemoveFromPlayZone();
            });
            cardSlots.ForEach(slot =>
            {
                CardBehaviour behaviour = slot.GetComponentInChildren<CardBehaviour>();
                behaviour.OnAddToPlayZone();
            });
        }
    }

    public void OnPlayHand()
    {
        if (cardSlots.Count > 0)
        {
            List<PlayingCardScript> cards = new();
            foreach (GameObject card in cardSlots)
            {

                cards.Add(card.GetComponentInChildren<PlayingCardScript>());
            }
            EventManager.EmitSubmitCards(cards);
            ClearCardSlots();
        }
    }

    private void ClearCardSlots()
    {
        button.SetActive(false);
        CardSlotsManager.ClearCardSlots(cardSlots);
        animator.SetBool("CardInPlayZone", false);
        animator.SetBool("HoverPlayZone", false);
    }

    internal void SetScrollOffset(float offset)
    {
        scrollOffset += offset;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayZoneScript : MonoBehaviour
{
    [SerializeField] private float targetSpaceBetweenCards;
    [SerializeField] private float handReAdjustmentSpeed;
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private Animator animator;
    [SerializeField] private string playZoneSlotName = "PlaySlot";
    [SerializeField] private GameObject button;
    private List<GameObject> cardSlots = new();

    private bool isHovered = false;

    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(false);
        targetPos = transform.localPosition;
        EventManager.DropCardInPlayZone += AddCardToPlayZone;
        EventManager.DropCardOutsidePlayZone += removeCardFromPlayZone;
        EventManager.SortSlots += SortCardSlots;
    }

    private void FixedUpdate()
    {
        CardSlotsManager.moveToAndRecalculateTargetPos(ref targetPos, transform, cardSlots.Count);
    }

    public void AddCardToPlayZone(GameObject cardSlot)
    {
        CardSlotsManager.AddCardToPlayCardSlots(cardSlot, cardSlots, transform, playZoneSlotName);

        button.SetActive(true);

        animator.SetBool("CardInPlayZone", true);

        cardSlot.GetComponentInChildren<CardSlotDeciderScript>().ResetPosition();
        cardSlot.GetComponentInChildren<CardBehaviour>().OnAddToPlayZone();
    }
    public void removeCardFromPlayZone(GameObject cardSlot)
    {
        CardSlotsManager.RemoveCardFromCardSlots(cardSlot, cardSlots, playZoneSlotName);

        animator.SetBool("CardInPlayZone", cardSlots.Count > 0);
        if (cardSlots.Count == 0)
        {
            button.SetActive(false);
        }
        //animator.SetTrigger("EndHoverPlayZone");

        cardSlot.GetComponentInChildren<CardBehaviour>().OnRemoveFromPlayZone();

    }
    public void SortCardSlots(GameObject initiator = null) => CardSlotsManager.SortCardSlots(cardSlots, initiator);

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isHovered && other.tag == "CardImage")
        {
            animator.SetBool("HoverPlayZone", true);
            isHovered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isHovered && other.tag == "CardImage")
        {
            isHovered = false;
            animator.SetBool("HoverPlayZone", false);

        }
    }

}

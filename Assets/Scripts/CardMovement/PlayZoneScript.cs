using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayZoneScript : MonoBehaviour
{
    [SerializeField] private float targetSpaceBetweenCards;
    [SerializeField] private float handReAdjustmentSpeed;
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private Animator animator;
    [SerializeField] private string playZoneSlotName = "PlaySlot";
    private List<GameObject> cardSlots = new();


    // Start is called before the first frame update
    void Start()
    {
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
        cardSlot.GetComponentInChildren<CardSlotDeciderScript>().ResetPosition();
    }

    public void removeCardFromPlayZone(GameObject cardSlot) => CardSlotsManager.RemoveCardFromCardSlots(cardSlot, cardSlots, playZoneSlotName);
    public void SortCardSlots(GameObject initiator = null) => CardSlotsManager.SortCardSlots(cardSlots, initiator);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CardImage")
        {
            Debug.Log(other.tag);
            animator.SetTrigger("HoverPlayZone");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "CardImage")
        {
            Debug.Log(other.tag);
            animator.SetTrigger("EndHoverPlayZone");
        }
    }

}

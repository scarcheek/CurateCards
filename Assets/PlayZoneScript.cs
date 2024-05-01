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
        EventManager.DropCardInPlayZone += addCardToPlayZone;
        EventManager.DropCardOutsidePlayZone += removeCardFromPlayZone;
        EventManager.SortSlots += SortCardSlots;
    }

    private void FixedUpdate()
    {
        // This is done to allow dynamic resizing of the cardslots
        targetPos = new Vector2(-(cardSlots.Count - 1) * targetSpaceBetweenCards / 2f, transform.localPosition.y);

        if (Vector3.Distance(transform.localPosition, targetPos) > 0.001f)
        {
            // Move our position a step closer to the target.
            var step = handReAdjustmentSpeed * Time.deltaTime; // calculate distance to move
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, step);
        }
    }

    public void addCardToPlayZone(GameObject cardSlot)
    {
        if (cardSlots.Contains(cardSlot)) return;

        Debug.Log("Trying to add card to play zone: " + cardSlot.name);
        cardSlot.transform.SetParent(transform);
        cardSlot.name = playZoneSlotName + cardSlots.Count;
        CardSlotDeciderScript csds = cardSlot.GetComponentInChildren<CardSlotDeciderScript>();
        csds.targetPos.x = cardSlots.Count * targetSpaceBetweenCards;
        csds.ResetPosition();
        targetPos.x -= (targetSpaceBetweenCards / 2);
        
        cardSlots.Add(cardSlot);
    }

    public void removeCardFromPlayZone(GameObject cardSlot)
    {
        if (!cardSlots.Contains(cardSlot)) return;


        cardSlots.Remove(cardSlot);
        for (int i = 0; i < cardSlots.Count; i++)
        {
            CardSlotsScript.RepositionCardSlot(cardSlots[i], playZoneSlotName, i);
            //cardSlots[i].GetComponentInChildren<CardSlotDeciderScript>().targetPos = new Vector3(targetSpaceBetweenCards * i, verticalSpacing);
        }
        SortCardSlots();
    }

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

    private void SortCardSlots(GameObject initiator = null)
    {
        if (initiator == null || cardSlots.Contains(initiator))
        {
            cardSlots.Sort(
                        (GameObject cardSlot, GameObject other) => cardSlot.name.CompareTo(other.name));
        }
    }
}

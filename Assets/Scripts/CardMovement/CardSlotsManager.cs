using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlotsManager
{
    public static float targetSpaceBetweenCards = 150f;
    public static float handReAdjustmentSpeed = 500f;
    public static float verticalSpacing = 50f;

    /// <summary>
    /// Calculates the targetPosition based on cardslotscount, with respect to the in-class targetSpaceBetweenCards and a transform with the handReAdjustmentSpeed
    /// </summary>
    /// <param name="targetPos">the reference to the targetPosition in the SlotsScript</param>
    /// <param name="transform">transform to take the localposition of</param>
    /// <param name="cardSlotsCount">How often to apply targetSpaceBetweenCards (Should always be CardSlots.Count)</param>
    public static void moveToAndRecalculateTargetPos(ref Vector2 targetPos, Transform transform, int cardSlotsCount)
    {
        // This is done to allow dynamic resizing of the cardslots
        targetPos = new Vector2(-(cardSlotsCount - 1) * targetSpaceBetweenCards / 2f, transform.localPosition.y);

        if (Vector3.Distance(transform.localPosition, targetPos) > 0.001f)
        {
            // Move our position a step closer to the target.
            var step = handReAdjustmentSpeed * Time.deltaTime; // calculate distance to move
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, step);
        }
    }

    /// <summary>
    /// Adds CardSlot to cardSlots and repositions it accordingly
    /// </summary>
    /// <param name="cardSlot">GameObject to be added</param>
    /// <param name="cardSlots">List of Gameobjects the gameObject has to be added to</param>
    /// <param name="transform">Transform the cardslot has to be parented to and repositioned around</param>
    /// <param name="nameTemplate">The new name of the Gameobject used for sorting and easier identification in the inspector</param>
    public static void AddCardToPlayCardSlots(GameObject cardSlot, List<GameObject> cardSlots, Transform transform, string nameTemplate)
    {
        if (cardSlots.Contains(cardSlot)) return;

        cardSlot.transform.SetParent(transform);

        CardSlotsManager.RepositionCardSlot(cardSlot, nameTemplate, cardSlots.Count);

        cardSlots.Add(cardSlot);
    }

    /// <summary>
    /// Removes the cardSlot from the list, repositions the cardSlots with RepositionCardSlots() and finally sorts the List
    /// </summary>
    /// <param name="cardSlot">gameobject to remove</param>
    /// <param name="cardSlots">List to remove gameobject from</param>
    /// <param name="nameTemplate">The name template used to name the cardslots after being re-ordered</param>
    public static void RemoveCardFromCardSlots(GameObject cardSlot, List<GameObject> cardSlots, string nameTemplate)
    {
        if (!cardSlots.Contains(cardSlot)) return;


        cardSlots.Remove(cardSlot);
        for (int i = 0; i < cardSlots.Count; i++)
        {
            RepositionCardSlot(cardSlots[i], nameTemplate, i);
        }
        SortCardSlots(cardSlots);
    }

    /// <summary>
    /// Renames the cardslot according to its position in cardslots, repositions it and finally sets the targetPos of the gameobject
    /// </summary>
    /// <param name="cardSlot">gameobject to reposition</param>
    /// <param name="nameTemplate">The name template used to name the cardslots after being re-ordered</param>
    /// <param name="pos">Position of the cardslot to take</param>
    public static void RepositionCardSlot(GameObject cardSlot, string nameTemplate, int pos)
    {
        cardSlot.name = nameTemplate + pos;
        cardSlot.transform.localPosition = new Vector3(targetSpaceBetweenCards * pos, verticalSpacing);

        cardSlot.GetComponentInChildren<PlayingCardScript>().card.SetPropPos(pos);
        Debug.Log(cardSlot.GetComponentInChildren<PlayingCardScript>().card.propPos);

        cardSlot.GetComponentInChildren<CardSlotDeciderScript>().targetPos = new Vector3(targetSpaceBetweenCards * pos, verticalSpacing);
    }

    /// <summary>
    /// Sorts cardslots based on their name
    /// </summary>
    /// <param name="cardSlots">List of gameobjects to be sorted</param>
    /// <param name="initiator">Default null. Set this if you want to make sure, it only sorts when the item is contained in the list</param>
    public static void SortCardSlots(List<GameObject> cardSlots, GameObject initiator = null)
    {
        if (initiator == null || cardSlots.Contains(initiator))
        {
            cardSlots.Sort(
                        (GameObject cardSlot, GameObject other) => cardSlot.name.CompareTo(other.name));
        }
    }

    public static void ClearCardSlots(List<GameObject> cardslots)
    {
        foreach (GameObject cardslot in cardslots)
        {
            GameObject.Destroy(cardslot);
        }
        cardslots.Clear();
    }
}

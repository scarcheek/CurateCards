using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSlotDeciderScript : MonoBehaviour
{
    [SerializeField] private float cardFollowSpeed;
    [SerializeField] private Transform cardTransform;
    [SerializeField] private float speed;

    public Vector3 targetPos;
    public Transform transformParent;

    private CardSlotsScript cardSlotsScript;
    private bool isDragged = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.parent.localPosition;
        cardSlotsScript = transform.parent.parent.GetComponent<CardSlotsScript>();
        // This should save some resources
        transformParent = transform.parent;
    }

    private void Update()
    {
        if (!isDragged && Vector2.Distance(transform.parent.localPosition, targetPos) > 0.001f)
        {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.parent.localPosition = Vector2.MoveTowards(transform.parent.localPosition, targetPos, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This prevents back and forth swapping
        if (isDragged && collision.tag == "CardSlot")
        { 
            CardSlotDeciderScript otherCardSlot = collision.GetComponent<CardSlotDeciderScript>();
            Transform otherTransformParent = otherCardSlot.transformParent;
            // Swap the target positions of the cards that collided
            (targetPos, otherCardSlot.targetPos) = (otherCardSlot.targetPos, targetPos);
            // Also swap the names to enable sorting of the cardSlotsScript, resulting in easier re-sorting after removing a card
            (transformParent.name, otherTransformParent.name) = (otherTransformParent.name, transformParent.name);
            cardSlotsScript.cardSlots.Sort(
                (GameObject cardSlot, GameObject other) => cardSlot.name.CompareTo(other.name));
           
        }
    }

    public void FollowCursor()
    {
        // OnMouseDrag did not work so this is an alternative, called from the event trigger on CardSlotDecider
        transform.position = new(Input.mousePosition.x, transform.position.y);
        cardTransform.position = Input.mousePosition;
        isDragged = true;
    }

    public void ReleaseCursor()
    {
        // OnMouseDragRelease did not work so this is an alternative
        transform.parent.localPosition = targetPos;
        transform.position = transform.parent.position;
        cardTransform.position = transform.position;
        isDragged = false;
    }
}

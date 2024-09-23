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
    [SerializeField] private BoxCollider2D cardImageCollider;
    [SerializeField] private BoxCollider2D playZoneCollider;
    public CardBehaviour cardBehaviour;

    private AudioManager audioManager;

    /// <summary>
    ///  TargetPos sets the localPosition of the gameobject it will move to with `speed`
    /// </summary>
    public Vector3 targetPos = Vector2.zero;
    public bool isDragged = false;
    [HideInInspector] public Transform transformParent;

    private CardSlotsScript cardSlotsScript;
    private BoxCollider2D cardSlotDeciderCollider;
    private bool isInPlayZone = false;

    // Start is called before the first frame update
    void Start()
    {
        cardBehaviour = GetComponentInChildren<CardBehaviour>();
        targetPos = transform.parent.localPosition;
        cardSlotsScript = transform.parent.parent.GetComponent<CardSlotsScript>();
        cardSlotDeciderCollider = GetComponent<BoxCollider2D>();
        // This should save some resources
        transformParent = transform.parent;

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
        if (isDragged)
        {
            // If the cardslotDecider collides with another card, triggering a swap
            if (collision.tag == "CardSlot" && collision.gameObject.layer == gameObject.layer)
            {
                CardSlotDeciderScript otherCardSlot = collision.GetComponent<CardSlotDeciderScript>();
                Transform otherTransformParent = otherCardSlot.transformParent;
                // Swap the target positions of the cards that collided
                (targetPos, otherCardSlot.targetPos) = (otherCardSlot.targetPos, targetPos);
                // Also swap the names to enable sorting of the cardSlotsScript, resulting in easier re-sorting after removing a card
                (transformParent.name, otherTransformParent.name) = (otherTransformParent.name, transformParent.name);

                (cardBehaviour.pos, otherCardSlot.cardBehaviour.pos) = (otherCardSlot.cardBehaviour.pos, cardBehaviour.pos);

                audioManager.PlaySFX("swap");
                EventManager.EmitOnSwapComplete(transformParent.gameObject);
            }
            // If the Card is being hovered over the PlayZone collider
            else if (collision.tag == "PlayZone")
            {
                isInPlayZone = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayZone")
        {
            isInPlayZone = false;
        }
    }

    /// <summary>
    /// Moves the cardSlotDecider with respect of the x value of the mouse and makes the image follow the mousePosition by setting `isDragged` to true
    /// As OnMouseDrag does not work on UI elements we have to use this workaround with the help of the event trigger component
    /// </summary>
    public void DragCursor()
    {
        // OnMouseDrag did not work so this is an alternative, called from the event trigger on CardSlotDecider
        transform.position = new(Input.mousePosition.x, transform.position.y);
        cardTransform.position = Input.mousePosition;
        isDragged = true;
    }

    /// <summary>
    /// Gets called if a card has been dragged and is then released, resulting in it having to be reset to the cardSlotDeciders Position
    /// </summary>
    public void ReleaseCursor()
    {
        isDragged = false;
        ResetPosition();
        if (isInPlayZone)
        {
            EventManager.EmitDropCardInPlayZone(transform.parent.gameObject);
        }
        else
        {
            EventManager.EmitDropCardOutsidePlayZone(transform.parent.gameObject);
        }
        audioManager.PlaySFX("putdown");
    }


    /// <summary>
    /// Resets all gameobjects of a CardSlot to the targetPosition
    /// </summary>
    public void ResetPosition()
    {
        //Position of cardslot
        transform.parent.localPosition = targetPos;
        //this position
        transform.position = transform.parent.position;
        //Position of card image
        cardTransform.position = transform.position;
    }
}

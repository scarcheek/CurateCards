using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSlotDeciderScript : MonoBehaviour
{
    [SerializeField] float cardFollowSpeed;
    [SerializeField] Transform cardTransform;
    [SerializeField] BoxCollider slotCollider;
    [SerializeField] float speed;


    bool isDragged = false;
    public Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.parent.localPosition;
    }

    private void Update()
    {
        // Check if the position of the cube and sphere are approximately equal.
        if (!isDragged && Vector2.Distance(transform.parent.localPosition, targetPos) > 0.001f)
        {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.parent.localPosition = Vector2.MoveTowards(transform.parent.localPosition, targetPos, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDragged)
        {
            CardSlotDeciderScript otherCardSlot = collision.GetComponent<CardSlotDeciderScript>();
            Vector2 swapper = targetPos;
            targetPos = otherCardSlot.targetPos;
            otherCardSlot.targetPos = swapper;
        }
    }

    public void FollowCursor()
    {
        transform.position = new(Input.mousePosition.x, transform.position.y);
        cardTransform.position = Input.mousePosition;
        isDragged = true;
    }

    public void ReleaseCursor()
    {
        transform.parent.localPosition = targetPos;
        transform.position = transform.parent.position;
        cardTransform.position = transform.position;
        isDragged = false;
    }
}

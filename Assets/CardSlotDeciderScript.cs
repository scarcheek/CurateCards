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

    bool isDragged = false;
    Vector3 targetPos;
    Vector3 lastSlotPos;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
        lastSlotPos = transform.parent.position;
    }

    public void FollowCursor()
    {
        transform.position = new(Input.mousePosition.x, transform.position.y);
        cardTransform.position = Input.mousePosition;
        isDragged = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDragged)
        {
            Debug.Log("Swapping positions with: " + collision.transform.parent.name + " " + transform.parent.localPosition + " and " + collision.transform.parent.localPosition);
            Vector3 swapper = transform.parent.localPosition;
            transform.parent.localPosition = collision.transform.parent.localPosition;
            collision.transform.parent.localPosition = swapper;

        }
    }

    public void ReleaseCursor()
    {
        //transform.position = new(Input.mousePosition.x, transform.position.y);
        transform.position = transform.parent.position;
        cardTransform.position = transform.position;
        isDragged = false;
    }
}

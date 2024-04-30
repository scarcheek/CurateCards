using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSlotsScript : MonoBehaviour
{
    [SerializeField] int DebugStartCardSlots = 4;
    [SerializeField] GameObject cardSlotPrefab;
    [SerializeField] float targetSpaceBetweenCards;
    [SerializeField] RectTransform canvas;
    [SerializeField] float handReAdjustmentSpeed;
    float spaceBetweenCards = 0;
    List<GameObject> cardSlots = new();
    [SerializeField] Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        
        targetPos = new(transform.position.x + targetSpaceBetweenCards/2, transform.position.y);
        
        //targetPos = new(transform.position.x + (DebugStartCardSlots % 2 == 0 ? targetSpaceBetweenCards/2 : 0) - (DebugStartCardSlots / 2 * targetSpaceBetweenCards) , transform.position.y);
        for (int i = 0; i < DebugStartCardSlots; i++)
        {
            addNewCardSlot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            drawCard();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            removeCard();
        }
    }

    private void FixedUpdate()
    {
        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, targetPos) > 0.001f)
        {
            // Move our position a step closer to the target.
            var step = handReAdjustmentSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
        }
    }

    private void drawCard()
    {
        // Do card handling
        addNewCardSlot();
    }

    private void removeCard()
    {
        GameObject.Destroy(cardSlots.Last().gameObject);
        cardSlots.Remove(cardSlots.Last());

        targetPos.x += targetSpaceBetweenCards / 2;
    }

    private void addNewCardSlot()
    {
        GameObject cardSlot = Instantiate(cardSlotPrefab, transform);
        cardSlot.name = cardSlotPrefab.name + cardSlots.Count;
        cardSlot.transform.localPosition = new Vector3(targetSpaceBetweenCards * cardSlots.Count, 50);
        cardSlots.Add(cardSlot);
        
        targetPos.x -=  targetSpaceBetweenCards / 2;
    }
}
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSlotsScript : MonoBehaviour
{
    [SerializeField] private int DebugStartCardSlots = 4;
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private float targetSpaceBetweenCards;
    [SerializeField] private float handReAdjustmentSpeed;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Canvas canvas;

    public List<GameObject> cardSlots = new();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < DebugStartCardSlots; i++)
        {
            CreateAndAddNewCardSlot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // Removes the last card as of right now, should be done with an event fired in some card-subscript
            RemoveCard();
        }
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

    private void DrawCard()
    {
        //TODO: Do card handling
        CreateAndAddNewCardSlot();
    }

    private void RemoveCard()
    {
        GameObject.Destroy(cardSlots.Last());
        int posIndexOfDelCard = (int)(cardSlots.Last().transform.localPosition.x / targetSpaceBetweenCards);
        for (int i = posIndexOfDelCard; i < posIndexOfDelCard; i++)
        {
            RepositionCardSlot(cardSlots[i]);
        }
        cardSlots.Remove(cardSlots.Last());
    }

    private void CreateAndAddNewCardSlot()
    {
        GameObject cardSlot = Instantiate(cardSlotPrefab, transform);
        cardSlot.name = cardSlotPrefab.name + cardSlots.Count;
        RepositionCardSlot(cardSlot);
        cardSlots.Add(cardSlot);
    }

    private void RepositionCardSlot(GameObject cardSlot)
    {
        cardSlot.transform.localPosition = new Vector3(targetSpaceBetweenCards * cardSlots.Count, 50);
    }
}
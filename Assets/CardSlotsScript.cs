using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSlotsScript : MonoBehaviour
{
    [SerializeField] public float handReAdjustmentSpeed;
    [SerializeField] private int DebugStartCardSlots = 4;
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private Canvas canvas;

    public List<GameObject> cardSlots = new();
    public static float targetSpaceBetweenCards = 100;
    private static float verticalSpacing = 50;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.DropCardInPlayZone += RemoveCard;
        EventManager.DropCardOutsidePlayZone += AddCardSlot;
        EventManager.SortSlots += SortCardSlots;


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

    private void RemoveCard(GameObject cardSlot)
    {
        if (!cardSlots.Contains(cardSlot)) return;

        cardSlots.Remove(cardSlot);
        for (int i = 0; i < cardSlots.Count; i++)
        {
            RepositionCardSlot(cardSlots[i], cardSlotPrefab.name, i);
            //cardSlots[i].GetComponentInChildren<CardSlotDeciderScript>().targetPos = new Vector3(targetSpaceBetweenCards * i, verticalSpacing);
        }
        SortCardSlots();
    }

    private void CreateAndAddNewCardSlot()
    {
        GameObject cardSlot = Instantiate(cardSlotPrefab, transform);
        AddCardSlot(cardSlot);
    }

    private void AddCardSlot(GameObject cardSlot)
    {
        if (!cardSlots.Contains(cardSlot))
        {
            cardSlot.transform.SetParent(transform);
            RepositionCardSlot(cardSlot, cardSlotPrefab.name, cardSlots.Count);
            cardSlots.Add(cardSlot);
        }
    }

    public static void RepositionCardSlot(GameObject cardSlot, string nameTemplate, int pos)
    {
        cardSlot.name = nameTemplate + pos;
        cardSlot.transform.localPosition = new Vector3(targetSpaceBetweenCards * pos, verticalSpacing);

        cardSlot.GetComponentInChildren<CardSlotDeciderScript>().targetPos = new Vector3(targetSpaceBetweenCards * pos, verticalSpacing);
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
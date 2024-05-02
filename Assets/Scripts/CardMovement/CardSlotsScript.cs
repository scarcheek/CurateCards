using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSlotsScript : MonoBehaviour
{
    [SerializeField] private int DebugStartCardSlots = 4;
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private Vector2 targetPos;

    public List<GameObject> cardSlots = new();

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard();
        }
    }

    private void FixedUpdate()
    {
        CardSlotsManager.moveToAndRecalculateTargetPos(ref targetPos, transform, cardSlots.Count);
    }

    private void DrawCard()
    {
        //TODO: Do card handling
        CreateAndAddNewCardSlot();
    }

    private void CreateAndAddNewCardSlot()
    {
        GameObject cardSlot = Instantiate(cardSlotPrefab, transform);
        AddCardSlot(cardSlot);
    }
    private void AddCardSlot(GameObject cardSlot) => CardSlotsManager.AddCardToPlayCardSlots(cardSlot, cardSlots, transform, cardSlotPrefab.name);
    private void RemoveCard(GameObject cardSlot) => CardSlotsManager.RemoveCardFromCardSlots(cardSlot, cardSlots, cardSlotPrefab.name);
    private void SortCardSlots(GameObject initiator = null) => CardSlotsManager.SortCardSlots(cardSlots, initiator);

}
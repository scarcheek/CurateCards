using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlotsScript : MonoBehaviour
{
    [SerializeField] int DebugStartCardSlots = 4;
    [SerializeField] GameObject cardSlotPrefab;
    [SerializeField] float targetSpaceBetweenCards;
    [SerializeField] RectTransform canvas;
    float spaceBetweenCards = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new(transform.position.x + (DebugStartCardSlots % 2 == 0 ? targetSpaceBetweenCards/2 : 0) - (DebugStartCardSlots / 2 * targetSpaceBetweenCards) , transform.position.y);
        for (int i = 0; i < DebugStartCardSlots; i++)
        {
            GameObject cardSlot = Instantiate(cardSlotPrefab, transform);
            cardSlot.transform.localPosition = new Vector3(targetSpaceBetweenCards * i, 50);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
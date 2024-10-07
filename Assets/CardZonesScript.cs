using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZonesScript : MonoBehaviour
{
    private PlayZoneScript playZoneSlots;
    private CardSlotsScript cardSlots;
    private BoxCollider2D playZoneCollider;
    // Start is called before the first frame update
    void Start()
    {
        cardSlots = GetComponentInChildren<CardSlotsScript>();
        playZoneSlots = GetComponentInChildren<PlayZoneScript>();
        playZoneCollider = GetComponentInChildren<BoxCollider2D>();
    }

    private void OnGUI()
    {
        if (Mathf.Abs(Input.mouseScrollDelta.y) > 0)
        {
            Vector2 point = Input.mousePosition;
            if (playZoneCollider.OverlapPoint(point))
            {
                Debug.Log("OnGUI overlap with point: " + point);
                playZoneSlots.SetScrollOffset(Input.mouseScrollDelta.y * ConfigManagerScript.instance.CardPanSpeed);
            }
            else
            {
                cardSlots.SetScrollOffset(Input.mouseScrollDelta.y * ConfigManagerScript.instance.CardPanSpeed);
            }
        }

    }
}

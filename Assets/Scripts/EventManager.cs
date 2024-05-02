using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<GameObject> DropCardInPlayZone;
    public static event Action<GameObject> DropCardOutsidePlayZone;
    public static event Action<GameObject> SortSlots;

    public static void EmitDropCardInPlayZone(GameObject cardSlot)
    {
        DropCardInPlayZone?.Invoke(cardSlot);
    }

    public static void EmitDropCardOutsidePlayZone(GameObject cardSlot)
    {
        DropCardOutsidePlayZone?.Invoke(cardSlot);
    }

    public static void EmitSortSlots(GameObject cardSlot)
    {
        SortSlots?.Invoke(cardSlot);
    }
}

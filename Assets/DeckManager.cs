using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private List<CardBehaviour> cards = new();

    public static DeckManager instance;
    private void Start()
    {
        instance = this;
    }
    public static List<CardBehaviour> GetRandomCardsOfAllTypes()
    {
        List<CardBehaviour> retCards = new()
        {
            GetRandomCardOfType(CardType.ancient),
            GetRandomCardOfType(CardType.technological),
            GetRandomCardOfType(CardType.contemporary),
            GetRandomCardOfType(CardType.everyday),
            GetRandomCardOfType(CardType.traditional)
        };
        return retCards;
    }

    public static CardBehaviour GetCardOfRandomType()
    {
        switch (Random.Range(0, 5))
        {
            case 0: return GetRandomCardOfType(CardType.ancient);
            case 1: return GetRandomCardOfType(CardType.contemporary);
            case 2: return GetRandomCardOfType(CardType.technological);
            case 3: return GetRandomCardOfType(CardType.traditional);
            case 4: return GetRandomCardOfType(CardType.everyday);
            default: return GetRandomCardOfType(CardType.ANY);
        }
    }

    private static CardBehaviour GetRandomCardOfType(CardType type)
    {
        List<CardBehaviour> typeCards = instance.cards.FindAll(card => card.cardProps.cardType.Contains(type));
        return typeCards[Random.Range(0, typeCards.Count)];
    }
}

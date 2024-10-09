using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<CardBehaviour> CardList = new();
    [SerializeField] private float _CardDrawCost;
    public float CardDrawCost { get { return _CardDrawCost; } }

    [SerializeField] private float _DeckRefreshCost;
    public float DeckRefreshCost { get { return _DeckRefreshCost; } }

    [SerializeField] private int _StartCardAmount;
    public int StartCardAmount { get { return _StartCardAmount; } }

    [Header("DEBUG")]
    public List<CardBehaviour> DeckList = new();
    public List<CardBehaviour> remainingCards = new();

    public static DeckManager instance;
    void Awake()
    {
        instance = this;

        DeckList.AddRange(GetRandomCardsOfAllTypes());
        remainingCards = DeckList.ToList();
        EventManager.StartDay += RepopulateRemainingCards;
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
        return GetRandomCardOfType(CardType.ANY);
    }

    public static CardBehaviour GetRandomCardOfType(CardType type)
    {
        List<CardBehaviour> typeCards = instance.CardList.FindAll(card => card.cardProps.cardType.Contains(type));
        return typeCards[Random.Range(0, typeCards.Count)];
    }

    internal static List<CardBehaviour> GetRandomCardsFromDeck(int amount)
    {
        List<CardBehaviour> retCards = new();
        for (int i = 0; i < amount; i++)
        {
            int indexRemoved = Random.Range(0, instance.remainingCards.Count);
            retCards.Add(instance.remainingCards[indexRemoved]);
            instance.remainingCards.RemoveAt(indexRemoved);
        }
        return retCards;
    }

    public static void RemoveCardFromDeckListAtIndex(int index)
    {
        instance.DeckList.RemoveAt(index);
    }

    public static void RefreshDeck()
    {

    }

    internal static void RepopulateRemainingCards()
    {
        Debug.Log("Repopulating RemainingCards");
        instance.remainingCards = instance.DeckList.ToList();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    private List<CardBehaviour> allCards;
    private List<CardBehaviour> deckCards;
    [SerializeField] private GameObject shopCardPrefab;
    [SerializeField] private GameObject removalCardPrefab;
    [SerializeField] private List<GameObject> shopCardSpots;
    [SerializeField] private GameObject removalCardSpot;
    private List<PlayingCardScript> selectedCards;
    private Animator animator;
    private PlayingCardScript cardToRemove;
    private int cardToRemoveIndex;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        EventManager.StartShopping += OnStartShopping;
    }

    private void OnStartShopping()
    {
        selectedCards = new();
        allCards ??= DeckManager.instance.CardList; // Ok now this is epic

        if (allCards != null)
            foreach (GameObject shopCardSpot in shopCardSpots)
            {
                if (shopCardSpot.transform.childCount == 0)
                {
                    GameObject shopCardObject = Instantiate(shopCardPrefab, shopCardSpot.transform);

                    ShopCardScript shopCard = shopCardObject.GetComponentInChildren<ShopCardScript>();
                    shopCard.behaviour = DeckManager.GetCardOfRandomType();
                }
            }

        deckCards ??= DeckManager.instance.DeckList;
        if (deckCards != null && removalCardSpot.transform.childCount == 0)
        {
            GameObject removalCardObject = Instantiate(removalCardPrefab, removalCardSpot.transform);
            
            RemovalCardScript shopCard = removalCardObject.GetComponentInChildren<RemovalCardScript>();
            cardToRemoveIndex = UnityEngine.Random.Range(0, deckCards.Count);
            shopCard.behaviour = DeckManager.instance.DeckList[cardToRemoveIndex];
        }

        animator.SetTrigger("StartShopping");
    }

    public void OnSelectCard(PlayingCardScript shopCard, bool selected)
    {
        if (!selected && selectedCards.Contains(shopCard))
        {
            selectedCards.Remove(shopCard);
            GameStateManager.instance.UpdateAvailableCoins(shopCard.card.cardProps.shopCost, 0);
        }
        else if (selected && !selectedCards.Contains(shopCard))
        {
            selectedCards.Add(shopCard);
            GameStateManager.instance.UpdateAvailableCoins(0, shopCard.card.cardProps.shopCost);

        }
        Debug.Log("Selected cards: " + selectedCards.Count);
    }

    public void OnRemovalCardClicked(PlayingCardScript removalCard,  bool selected)
    {
        if (selected)
        {
            cardToRemove = removalCard;
        } else
        {
            cardToRemove = null;
        }
    }

    public void BuyCards()
    {
        foreach (PlayingCardScript card in selectedCards)
        {
            DeckManager.instance.DeckList.Add(card.card);
            card.transform.parent.SetParent(null);
            card.transform.parent.gameObject.SetActive(false);
        }
    }

    public void FinishedShopping()
    {
        foreach (GameObject shopCardSpot in shopCardSpots)
        {
            for (int i = 0; i < shopCardSpot.transform.childCount; i++)
            {
                Destroy(shopCardSpot.transform.GetChild(i).gameObject);
            }
        }
        if (cardToRemove != null)
        {
            DeckManager.RemoveCardFromDeckListAtIndex(cardToRemoveIndex);
        }
        Destroy(removalCardSpot.transform.GetChild(0).gameObject);
        animator.SetTrigger("StopShopping");
    }

    private void AnimationStopShoppingDone() 
    {
        EventManager.EmitStartDay();
        
    }

}

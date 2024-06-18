using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    private List<CardBehaviour> allCards;
    [SerializeField] private GameObject shopCardPrefab;
    [SerializeField] private GameObject removalCardSpot;
    [SerializeField] private List<GameObject> shopCardSpots;
    private List<PlayingCardScript> selectedCards;
    private Animator animator;
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

        animator.SetTrigger("StopShopping");
    }

    private void AnimationStopShoppingDone() 
    {
        EventManager.EmitStartDay();
        
    }

}

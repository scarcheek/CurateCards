using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    private List<CardBehaviour> allCards;
    [SerializeField] private GameObject shopCardPrefab;
    [SerializeField] private List<GameObject> ShopCardSpots;
    private List<PlayingCardScript> selectedCards;
    private Animator animator;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        selectedCards = new();
        if(allCards == null) allCards = DeckManager.instance.CardList;

        if (allCards != null)
            foreach (GameObject shopCardSpot in ShopCardSpots)
            {
                GameObject shopCardObject = Instantiate(shopCardPrefab, shopCardSpot.transform);

                ShopCardScript shopCard = shopCardObject.GetComponentInChildren<ShopCardScript>();
                shopCard.behaviour = DeckManager.GetCardOfRandomType();
            }
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
        animator.SetTrigger("StopShopping");

        foreach (GameObject shopCardSpot in ShopCardSpots)
        {
            Destroy(shopCardSpot.transform.GetChild(0).gameObject);
            
        }
    }

    private void AnimationStopShoppingDone() 
    {
        EventManager.EmitStartDay();
        
    }

}

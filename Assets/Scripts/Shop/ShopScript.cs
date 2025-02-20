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
    [SerializeField] public float RerollCost = 50;


    [SerializeField] private GameObject shopCardPrefab;
    [SerializeField] private GameObject removalCardPrefab;
    [SerializeField] private List<GameObject> shopCardSpots;
    [SerializeField] private GameObject removalCardSpot;
    [SerializeField] private TextMeshProUGUI spentCoinsText;
    [SerializeField] private TextMeshProUGUI rerollCostText;
    private float spentCoins = 0;


    private List<PlayingCardScript> selectedCards;
    private Animator animator;
    private PlayingCardScript cardToRemove;
    private int cardToRemoveIndex;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        EventManager.StartShopping += OnStartShopping;
        rerollCostText.text = RerollCost.ToString();
    }
    
    private void OnStartShopping()
    {
        PopulateCardSlots();

        animator.SetTrigger("StartShopping");
    }

    private void PopulateCardSlots(bool repopulate = false)
    {
        spentCoins = 0;
        UpdateSpentCoinsText();
        selectedCards = new();
        allCards ??= DeckManager.instance.CardList; // Ok now this is epic

        if (allCards != null)
            foreach (GameObject shopCardSpot in shopCardSpots)
            {
                if (repopulate && shopCardSpot.transform.childCount > 0)
                {
                    Destroy(shopCardSpot.transform.GetChild(0).gameObject);
                }
                if (repopulate || shopCardSpot.transform.childCount == 0)
                {
                    GameObject shopCardObject = Instantiate(shopCardPrefab, shopCardSpot.transform);

                    ShopCardScript shopCard = shopCardObject.GetComponentInChildren<ShopCardScript>();
                    shopCard.behaviour = DeckManager.GetCardOfRandomType();
                }
            }

        deckCards ??= DeckManager.instance.DeckList;
        if (repopulate && removalCardSpot.transform.childCount > 0) Destroy(removalCardSpot.transform.GetChild(0).gameObject);
        if (deckCards != null && (repopulate || removalCardSpot.transform.childCount == 0))
        {
            GameObject removalCardObject = Instantiate(removalCardPrefab, removalCardSpot.transform);

            RemovalCardScript shopCard = removalCardObject.GetComponentInChildren<RemovalCardScript>();
            cardToRemoveIndex = UnityEngine.Random.Range(0, deckCards.Count);
            shopCard.behaviour = DeckManager.instance.DeckList[cardToRemoveIndex];
        }
    }

    public void OnSelectCard(PlayingCardScript shopCard, bool selected)
    {
        if (!selected && selectedCards.Contains(shopCard))
        {
            selectedCards.Remove(shopCard);
            spentCoins += shopCard.card.cardProps.shopCost;
        }
        else if (selected && !selectedCards.Contains(shopCard))
        {
            selectedCards.Add(shopCard);
            spentCoins -= shopCard.card.cardProps.shopCost;
        }

        UpdateSpentCoinsText();
        Debug.Log("Spent coins: " + spentCoins);
    }

    public void OnRemovalCardClicked(PlayingCardScript removalCard, bool selected)
    {
        if (selected)
        {
            cardToRemove = removalCard;
        }
        else
        {
            cardToRemove = null;
        }
    }

    public void BuyCards()
    {
        GameStateManager.instance.UpdateAvailableCoins(spentCoins, 0);
        spentCoins = 0;

        UpdateSpentCoinsText();

        foreach (PlayingCardScript card in selectedCards)
        {
            DeckManager.instance.DeckList.Add(card.card);
            card.transform.parent.SetParent(null);
            card.transform.parent.gameObject.SetActive(false);
        }
    }

    public void RerollCards()
    {
        GameStateManager.AvailableCoins -= RerollCost;

        PopulateCardSlots(true);

    }

    public void FinishedShopping()
    {
        BuyCards();
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

    private void AnimationStopShoppingDone() => EventManager.EmitStartDay();

    private void UpdateSpentCoinsText()
    {
        spentCoinsText.gameObject.SetActive(spentCoins < 0);
        spentCoinsText.text = spentCoins.ToString();
    }

    private void OnDestroy()
    {
        EventManager.StartShopping -= OnStartShopping;
    }
}

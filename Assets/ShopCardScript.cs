using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopCardScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI salesTagText;

    [HideInInspector] public CardBehaviour behaviour;
    PlayingCardScript playingCard;
    ShopScript parentScript;
    [SerializeField] bool selected;
    // Start is called before the first frame update
    void Start()
    {
        playingCard = GetComponentInChildren<PlayingCardScript>();
        parentScript = GetComponentInParent<ShopScript>();
        CardBehaviour cardBehaviour = Instantiate(behaviour, playingCard.transform);

        salesTagText.text = behaviour.cardProps.shopCost.ToString();
        playingCard.card = cardBehaviour;    
    }


    public void OnClick()
    {
        selected = !selected;
        HandleSelect();
    }

    private void HandleSelect()
    {
        if (selected)
        {
            transform.localRotation = Quaternion.Euler(0, 0, -7);
        }
        else
        {
            transform.localRotation = Quaternion.identity;
        }
        parentScript.OnSelectCard(playingCard, selected);
    }
}

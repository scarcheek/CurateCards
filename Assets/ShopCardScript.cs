using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopCardScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI salesTagText;

    [HideInInspector] public CardBehaviour behaviour;
    protected PlayingCardScript playingCard;
    protected ShopScript parentScript;
    protected bool selected;
    protected Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playingCard = GetComponentInChildren<PlayingCardScript>();
        parentScript = GetComponentInParent<ShopScript>();
        CardBehaviour cardBehaviour = Instantiate(behaviour, playingCard.transform);
        playingCard.card = cardBehaviour;    

        if (salesTagText != null ) salesTagText.text = behaviour.cardProps.shopCost.ToString();
    }


    public void OnClick()
    {
        selected = !selected;
        HandleSelect();
    }

    public virtual void HandleSelect()
    {
        VisualizeSelect();
        parentScript.OnSelectCard(playingCard, selected);
    }

    internal void VisualizeSelect()
    {
        if (selected)
        {
            transform.localRotation = Quaternion.Euler(0, 0, -7);
        }
        else
        {
            transform.localRotation = Quaternion.identity;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayingCardScript : MonoBehaviour
{
    [Header("Props")]
    private bool isHovered = false;
    [HideInInspector] public CardProps card;
    [Header("Component References")]
    [SerializeField] private Image SplashArt;
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private TextMeshProUGUI CostText;
    [SerializeField] private TextMeshProUGUI BaseValueText;
    [SerializeField] private TextMeshProUGUI TypeMediumText;

    // Start is called before the first frame update
    
    void Start()
    {
        SplashArt.sprite = card.cardSprite;
        TitleText.text = card.title;
        DescriptionText.text = StylizeDescription(card);
        CostText.text = card.cost.ToString();
        BaseValueText.text = card.baseValue.ToString();
        StringBuilder sb = new StringBuilder();

        int i;
        bool hasNext;
        if (card.cardType.Count > 0)
        {
            i = 0;
            do
            {
                sb.Append(card.cardType[i].ToString());
                i++;

                hasNext = i < card.cardType.Count;
                if (hasNext) sb.Append(", ");
            } while (hasNext);
        }
        if (card.medium.Count > 0)
        {
            sb.Append(" - ");
            
            i = 0;
            do
            {
                sb.Append(card.medium[i].ToString());
                i++;

                hasNext = i < card.medium.Count;
                if (hasNext) sb.Append(", ");
            } while (hasNext);
        }
        TypeMediumText.text = sb.ToString();
    }
    
    private string StylizeDescription(CardProps card)
    {
        string cardDescription = card.description;
        foreach (Effect effect in card.effects)
        {
            cardDescription = cardDescription.Replace("{", CardEffectColors.effectColors[effect]);
            cardDescription = cardDescription.Replace("}", CardEffectColors.effectColors[Effect.ENDSTYLE]);
        }
        return cardDescription;
    }

    public void OnPointerEnter()
    {
        if (!isHovered)
        {
            isHovered = true;
        }
    }

    public void OnPointerExit()
    {
        if(isHovered)
        {
            isHovered = false;
        }
    }
}

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
    [HideInInspector] public CardBehaviour card;
    [Header("Component References")]
    [SerializeField] private Image SplashArt;
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private TextMeshProUGUI CostText;
    [SerializeField] private TextMeshProUGUI BaseValueText;
    [SerializeField] private TextMeshProUGUI TypeMediumText;
    private Color defaultValueColor;
    private Color defaultCostColor;

    // Start is called before the first frame update

    void Start()
    {
        InitializeCardView();
        defaultValueColor = BaseValueText.color;
        defaultCostColor = CostText.color;
    }

    public void InitializeCardView()
    {
        DisplayCardProps();
        SetTypeAndMedium();
    }

    public void DisplayCardProps()
    {
        SplashArt.sprite = card.cardProps.cardSprite;
        TitleText.text = card.cardProps.title;
        DescriptionText.text = StylizeDescription(card.cardProps);
    }

    public void DisplayCardStats(float cardCost, float cardValue, bool crits = false)
    {
        CostText.text = cardCost.ToString();
        BaseValueText.text = cardValue.ToString();

        if (crits)
        {
            BaseValueText.color = ConfigManagerScript.instance.posEffectColor;
        }
        else
        {
            BaseValueText.color = defaultValueColor;
        }

        if (card.cardProps.cost > cardCost)
        {
            CostText.color = ConfigManagerScript.instance.posEffectColor;
        }
        else if (card.cardProps.cost == cardCost)
        {
            CostText.color = defaultCostColor;
        }
        else
        {
            CostText.color = ConfigManagerScript.instance.negativeColor;
        }
    }

    public void SetTypeAndMedium()
    {
        StringBuilder sb = new StringBuilder();

        int i;
        bool hasNext;
        if (card.cardProps.cardType.Count > 0)
        {
            i = 0;
            do
            {
                if (card.cardProps.cardType[i] != CardType.ANY)
                    sb.Append(card.cardProps.cardType[i].ToString());
                i++;

                hasNext = i < card.cardProps.cardType.Count -1;
                if (hasNext) sb.Append(", ");
            } while (hasNext);
        }
        if (card.cardProps.medium.Count > 0)
        {
            sb.Append(" - ");

            i = 0;
            do
            {
                sb.Append(card.cardProps.medium[i].ToString());
                i++;

                hasNext = i < card.cardProps.medium.Count;
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
            cardDescription = ReplaceFirst(cardDescription, "{", CardEffects.effectColors[effect]);
            cardDescription = ReplaceFirst(cardDescription, "}", CardEffects.effectColors[Effect.ENDSTYLE]);
        }
        return cardDescription;
    }

    private string ReplaceFirst(string text, string search, string replace)
    {
        int pos = text.IndexOf(search);
        if (pos < 0)
        {
            return text;
        }
        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
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
        if (isHovered)
        {
            isHovered = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayingCardScript : MonoBehaviour
{
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
        DescriptionText.text = card.description;
        CostText.text = card.cost.ToString();
        BaseValueText.text = card.baseValue.ToString();
        StringBuilder sb = new StringBuilder();
        sb.Append(card.cardType.ToString());
        if (card.medium != Medium.none)
        {
            sb.Append(" - ");
            sb.Append(card.medium.ToString());
        }
        TypeMediumText.text = sb.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

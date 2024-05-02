using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingCardScript : MonoBehaviour
{
    [HideInInspector] public CardProps card;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = card.cardSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mainCameraScript : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private List<PlayingCardScript> remainingCards;
    private void Start()
    {
        EventManager.submitCards += OnPlayCards;
    }


    private void OnPlayCards(List<PlayingCardScript> cards)
    {
        Debug.Log("Wos is?");
        anim.SetTrigger("Present");
        remainingCards = new List<PlayingCardScript>(cards);
        anim.SetBool("AreCardsLeft", true);
    }

    public void OnPresent()
    {
        PlayingCardScript cardToPlay = remainingCards[0];
        remainingCards.RemoveAt(0);
        EventManager.EmitPresentCard(cardToPlay);
        anim.SetBool("AreCardsLeft", remainingCards.Count > 0);
    }

    public void PresentVisitorDone()
    {
        EventManager.EmitAnimationVisitorDone();
    }
}

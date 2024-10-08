using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mainCameraScript : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private List<PlayingCardScript> remainingCards = new();
    private PlayingCardScript currentlyPresenting;
    private void Start()
    {
        EventManager.submitCards += OnSubmitCards;
    }


    private void OnSubmitCards(List<PlayingCardScript> cards)
    {
        anim.SetTrigger("Present");
        remainingCards = new(cards);
        anim.SetBool("AreCardsLeft", true);
    }

    public void OnPresent()
    {
        PlayingCardScript cardToPlay = remainingCards[0];
        remainingCards.RemoveAt(0);
        currentlyPresenting = cardToPlay;
        EventManager.EmitPresentCard(cardToPlay);
        if (remainingCards.Count == 0) 
        { 
            anim.SetBool("AreCardsLeft", false);
        }
    }

    public void PresentVisitorDone()
    {
        EventManager.EmitAnimationVisitorDone();
    }

    public void OnCurationDone()
    {
        EventManager.EmitCurationDone();
    }

    /// <summary>
    /// start when show visitors animation is run :)
    /// </summary>
    public void PresentVisitorStart()
    {
        currentlyPresenting.card.Play();
        currentlyPresenting = null;
    }

    public void CheckThrow()
    {
        EventManager.EmitCheckThrow();
    }
}

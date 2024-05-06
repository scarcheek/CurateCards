using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtSpotScript : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private ParticleSystem smokeEffect;

    private void Start()
    {
        EventManager.PlayCards += OnPlayCards;
    }

    private void OnPlayCards(List<PlayingCardScript> cards)
    {
        smokeEffect.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArtSpotScript : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private ParticleSystem smokeEffect;
    private GameObject artPiece;

    private void Start()
    {
        EventManager.PresentCard += OnPresent;
        EventManager.ShowVisitorDone += OnShowVisitorDone;
    }

    private void OnPresent(PlayingCardScript card)
    {
        smokeEffect.Play();
        if (card.card.presentPrefab) 
            artPiece = Instantiate(card.card.presentPrefab);
    }

    private void OnShowVisitorDone()
    {
        if (artPiece != null) Destroy(artPiece);
        artPiece = null;
        smokeEffect.Play();
    }
}
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
        EventManager.AnimationVisitorDone += OnShowVisitorDone;
    }

    private void OnPresent(PlayingCardScript card)
    {
        smokeEffect.Play();
        if (card.card.cardProps.presentPrefab)
        {
            Debug.Log("presenting");
            artPiece = Instantiate(card.card.cardProps.presentPrefab, transform, false);
            if (card.card.cardProps.medium.Contains(Medium.painting) || card.card.cardProps.medium.Contains(Medium.music) || card.card.cardProps.medium.Contains(Medium.videogame))
            {
                artPiece.transform.localPosition = new Vector3(0.0f, 1, 0.0f); 
            }
        }
    }

    private void OnShowVisitorDone()
    {
        if (artPiece != null) Destroy(artPiece);
        artPiece = null;
        smokeEffect.Play();
    }
}

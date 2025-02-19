using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArtSpotScript : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private ParticleSystem smokeEffect;
    private GameObject artPiece;
    [SerializeField]private int throwChance = 0; // in %
    [SerializeField] private Vector3 throwDriection = new Vector3(-5, 7, 0);
    [SerializeField] private float directionVariation = 0f;

    private void Start()
    {
        EventManager.PresentCard += OnPresent;
        EventManager.AnimationVisitorDone += OnShowVisitorDone;
        EventManager.CheckThrow += OnCheckThrow;
    }

    private void OnPresent(PlayingCardScript card)
    {
        smokeEffect.Play();
        if (card.card.cardProps.presentPrefab)
        {
            //Debug.Log("presenting");
            artPiece = Instantiate(card.card.cardProps.presentPrefab, transform, false);
        }
    }

    private void OnCheckThrow()
    {
        int throwRN = Random.Range(0, 100);
        if (throwRN < throwChance)
        {
            Debug.Log("Throw");
            ThrowArtpiece();
        }
    }
    private void ThrowArtpiece()
    {

        if (artPiece != null)
        {
            Rigidbody rb = artPiece.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            Vector3 randomThrow = new Vector3((Random.value - 0.5f)*2 * directionVariation, (Random.value - 0.5f)*2 * directionVariation, (Random.value - 0.5f)*2 * directionVariation);
            rb.velocity = throwDriection + randomThrow;
            rb.AddTorque(new Vector3(0,0,10));
            //Debug.Log(throwDriection + " " + randomThrow);
        }
    }

    private void OnShowVisitorDone()
    {
        if (artPiece != null) Destroy(artPiece);
        artPiece = null;
        smokeEffect.Play();
        AudioManager.instance.Unmute();
    }

    private void OnDestroy()
    {
        EventManager.PresentCard -= OnPresent;
        EventManager.AnimationVisitorDone -= OnShowVisitorDone;
        EventManager.CheckThrow -= OnCheckThrow;
    }
}

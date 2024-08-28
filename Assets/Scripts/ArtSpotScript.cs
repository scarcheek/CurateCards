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

    private void Start()
    {
        EventManager.PresentCard += OnPresent;
        EventManager.AnimationVisitorDone += OnShowVisitorDone;
    }

    private void OnPresent(PlayingCardScript card)
    {
        //smokeEffect.Play();
        if (card.card.cardProps.presentPrefab)
        {
            Debug.Log("presenting");
            artPiece = Instantiate(card.card.cardProps.presentPrefab, transform, false);
            Debug.Log("name: "+artPiece.name + ",  is kinematic: " + artPiece.GetComponent<Rigidbody>().isKinematic);

            //this will throw the artpiece
            int throwRN = Random.Range(0, 100);
            Debug.Log("ThrowRN = " + throwRN + " < " + throwChance +": " + (throwRN < throwChance));
            if (throwRN < throwChance)
            {
                Debug.Log("Throw");
                ThrowArtpiece();
            }
        }
    }

    private void ThrowArtpiece()
    {
        if (artPiece != null)
        {
            Rigidbody rb = artPiece.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.velocity = throwDriection;
            rb.AddTorque(new Vector3(0,0,10));
        }
    }

    private void OnShowVisitorDone()
    {
        if (artPiece != null) Destroy(artPiece);
        artPiece = null;
        smokeEffect.Play();
    }
}

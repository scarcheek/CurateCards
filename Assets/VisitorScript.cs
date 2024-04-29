using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer bodySprite;
    [SerializeField] SpriteRenderer hatSprite;
    [SerializeField] float hatProb;

    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("visitor_idle_default", -1, Random.value);
        hatSprite.enabled = Random.value > hatProb;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

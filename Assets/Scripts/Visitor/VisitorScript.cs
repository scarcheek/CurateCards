using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorScript : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private SpriteRenderer bodySprite;
    [SerializeField] private SpriteRenderer hatSprite;
    [SerializeField] private float hatProb;

    public Vector3 standPos;

    private Animator anim;
    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.Play("visitor_idle_default", -1, Random.value);
        hatSprite.enabled = Random.value > hatProb;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, standPos) > 0.001f)
        {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, standPos, step);
        }
    }
}

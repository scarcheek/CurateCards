using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer bodySprite;
    [SerializeField] SpriteRenderer hatSprite;
    [SerializeField] float hatProb;
    [SerializeField] public float speed;

    public Vector3 standPos;

    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.Play("visitor_idle_default", -1, Random.value);
        hatSprite.enabled = Random.value > hatProb;
        // standPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, standPos) > 0.001f)
        {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, standPos, step);
        }
    }
}

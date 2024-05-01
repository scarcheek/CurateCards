using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayZoneScript : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CardImage")
        {
            Debug.Log(other.tag);
            animator.SetTrigger("HoverPlayZone");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "CardImage")
        {
            Debug.Log(other.tag);
            animator.SetTrigger("EndHoverPlayZone");
        }
    }
}

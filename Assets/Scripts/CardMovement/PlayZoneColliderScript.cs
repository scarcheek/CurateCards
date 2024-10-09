using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayZoneColliderScript : MonoBehaviour
{
    private bool isHovered = false;
    [SerializeField] PlayZoneScript playZone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isHovered && other.tag == "CardImage")
        {
            playZone.animator.SetBool("HoverPlayZone", true);
            isHovered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isHovered && other.tag == "CardImage")
        {
            isHovered = false;
            playZone.animator.SetBool("HoverPlayZone", false);
                
        }
    }
}

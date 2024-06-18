using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovalCardScript : ShopCardScript
{
    public override void HandleSelect()
    {
        if(selected)
        {
            anim.Play("PutCardIntoTrash");
        } else
        {
            anim.Play("GetCardBackFromTrash");
        }
        parentScript.OnRemovalCardClicked(playingCard, selected);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkCameraScript : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        EventManager.AnimationVisitorDone += showVisitors;
    }

    void showVisitors()
    {
        anim.SetTrigger("showVisitors");
    }


}

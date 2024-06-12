using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkCameraScript : MonoBehaviour
{
    Animator anim;
    bool forceShowVisitors = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        EventManager.AnimationVisitorDone += showVisitors;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            forceShowVisitors = true;
            anim.SetBool("showVisitors", forceShowVisitors);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            forceShowVisitors = false;
            anim.SetBool("showVisitors", forceShowVisitors);
        }
    }

    void showVisitors()
    {
        anim.SetBool("showVisitors", true);
    }

    void stopShowVisitors()
    {
        anim.SetBool("showVisitors", forceShowVisitors);

    }


}

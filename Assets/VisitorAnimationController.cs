using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorAnimationController : MonoBehaviour
{
    public Animator anim;
    private VisitorScript parentScript;


    // Start is called before the first frame update
    void Start()
    {
        parentScript = GetComponentInParent<VisitorScript>();
        anim = GetComponent<Animator>();
        anim.Play("visitor_idle_default", -1, Random.value);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetTrigger("backflip");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetTrigger("shakefall");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetTrigger("falloverback");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            anim.SetTrigger("leftright");
        }
    }
    internal void ReactToScore(float threshhold, float score)
    {
        if (score < -threshhold)
        {
            anim.SetTrigger("shakefall");
        }
        else if (score > threshhold)
        {
            anim.SetTrigger("backflip");
        }
        else
        {
            anim.SetTrigger("leftright");

        }
    }

    public void OnShakeAndFallDone()
    {
        parentScript.ReactionDone();
    }

    public void OnFallOverDone()
    {
        parentScript.ReactionDone();

    }

    public void OnBackFlipDone()
    {
        parentScript.ReactionDone();
    }
    public void OnLeftRightDone()
    {
        parentScript.ReactionDone();
    }

}

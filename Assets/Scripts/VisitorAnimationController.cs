using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorAnimationController : MonoBehaviour
{
    public Animator anim;
    private VisitorScript parentScript;
    private AudioSource source;

    private VisitorSounds visitorSounds;


    // Start is called before the first frame update
    void Start()
    {
        parentScript = GetComponentInParent<VisitorScript>();
        anim = GetComponent<Animator>();
        source = GetComponentInParent<AudioSource>();

        visitorSounds = AudioManager.instance.GetComponent<VisitorSounds>();

        anim.Play("visitor_idle_default", -1, Random.value);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetTrigger("backflip");
            source.clip = visitorSounds.randomHappy();
            source.Play();

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetTrigger("shakefall");
            source.clip = visitorSounds.randomAngry();
            source.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetTrigger("falloverback");
            source.clip = visitorSounds.randomThud();
            source.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            anim.SetTrigger("leftright");
            source.clip = visitorSounds.randomNeutral();
            source.Play();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            anim.SetTrigger("stopmoving");
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            anim.SetTrigger("startmoving");
        }
    
    }
    internal void ReactToScore(float threshhold, float score)
    {
        if (score < -threshhold)
        {
            anim.SetTrigger("shakefall");
            source.clip = visitorSounds.randomAngry();
            source.Play();
        }
        else if (score > threshhold)
        {
            anim.SetTrigger("backflip");
            source.clip = visitorSounds.randomHappy();
            source.Play();
        }
        else
        {
            anim.SetTrigger("leftright");
            source.clip = visitorSounds.randomNeutral();
            source.Play();
        }
    }

    public void ThudSound()
    {
        source.clip = visitorSounds.randomThud();
        source.Play();
    }
    public void OnShakeAndFallDone()
    {
        parentScript.ReactionDone();
    }

    public void OnFallOverDone()
    {
        Debug.Log("OnFallOverDone called");
        parentScript.OnFallOverBackDone();

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

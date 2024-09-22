using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorAnimationController : MonoBehaviour
{
    public Animator anim;
    private VisitorScript parentScript;
    private AudioSource source;

    private VisitorSounds sounds;


    // Start is called before the first frame update
    void Start()
    {
        parentScript = GetComponentInParent<VisitorScript>();
        anim = GetComponent<Animator>();
        source = GetComponentInParent<AudioSource>();

        sounds = GameStateManager.instance.GetComponent<VisitorSounds>();

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
            source.clip = sounds.randomAngry();
            source.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetTrigger("falloverback");
            source.clip = sounds.randomThud();
            source.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            anim.SetTrigger("leftright");
            source.clip = sounds.randomNeutral();
            source.Play();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            anim.speed = 0;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            anim.speed = 1;
        }
    
    }
    internal void ReactToScore(float threshhold, float score)
    {
        if (score < -threshhold)
        {
            anim.SetTrigger("shakefall");
            source.clip = sounds.randomAngry();
            source.Play();
        }
        else if (score > threshhold)
        {
            anim.SetTrigger("backflip");
            source.clip = sounds.randomHappy();
            source.Play();
        }
        else
        {
            anim.SetTrigger("leftright");
            source.clip = sounds.randomNeutral();
            source.Play();
        }
    }

    public void ThudSound()
    {
        source.clip = sounds.randomThud();
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

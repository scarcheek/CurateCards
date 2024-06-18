using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnnouncementsScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CelebrationText;
    [SerializeField] private TextMeshProUGUI FailureText;
    [SerializeField] private ParticleSystem CelebrationParticles;
    [SerializeField] private ParticleSystem FailureParticles;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        EventManager.CelebrateDayComplete += OnCelebrateDayComplete;
        EventManager.RunFailed += OnRunFailed;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCelebrateDayComplete()
    {
        CelebrationParticles.Play();
        anim.SetTrigger("DayComplete");
    }
    private void OnRunFailed(string reason)
    {
        Debug.Log("Fuck man the run ended with reason: " + reason);
        FailureText.text = reason;
        FailureParticles.Play();
        anim.SetTrigger("RunFailed");
    }
    private void OnAnimationComplete()
    {
        EventManager.EmitStartShopping();
    }
}

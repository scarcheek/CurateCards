using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnnouncementsScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CelebrationText;
    [SerializeField] private ParticleSystem CelebrationParticles;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        EventManager.DayComplete += OnDayComplete;
    }

    private void OnDayComplete()
    {
        CelebrationParticles.Play();
        anim.SetTrigger("DayComplete");
    }
}

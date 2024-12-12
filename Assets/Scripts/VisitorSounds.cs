using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] happy;
    [SerializeField] private AudioClip[] angry;
    [SerializeField] private AudioClip[] neutral;
    [SerializeField] private AudioClip[] thud;

    public static VisitorSounds instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public AudioClip randomHappy()
    {
        return happy[Random.Range(0, happy.Length)];
    }

    public AudioClip randomAngry()
    {
        return angry[Random.Range(0, angry.Length)];
    }

    public AudioClip randomNeutral()
    {
        return neutral[Random.Range(0, neutral.Length)];
    }

    public AudioClip randomThud()
    {
        return thud[Random.Range(0, thud.Length)];
    }
}

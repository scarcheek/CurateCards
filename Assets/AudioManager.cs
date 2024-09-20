using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    private Sound playing;
    private Sound ending;



    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        PlaySong("thinking", true);
    }
    
    public void PlaySong(string name, bool fade)  
    {
        Debug.Log("does at least try");
        Sound s = Array.Find(sounds, s => s.name == name);
        if (s == null)
        {
            Debug.Log("couldnt find audio: " + name);
            return;
        }
        if (playing != null) playing.source.Stop();
        playing = s;
        playing.source.Play();
    }


    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sounds, s => s.name == name);
        if (s == null) 
        {
            Debug.Log("couldnt find audio: " + name);
            return;
        }
            
        s.source.Play();
    }

    void Update()
    {
    }
}

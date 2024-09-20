using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    public Sound[] songs;
    public Sound[] sfx;

    private Sound playing;
    private Sound ending;

    public AudioMixer mixer;
    public AudioMixerGroup endGroup;
    public AudioMixerGroup playGroup;
    public AudioMixerGroup uiGroup;

    private Coroutine fading;
    private bool muted = false;

    [SerializeField] private float SongVolume;
    [SerializeField] private float SfxVolume;
    [SerializeField] private float fadeTime;

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

        EventManager.PresentCard += OnPresent;

        foreach (Sound s in songs)
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
        Sound s = Array.Find(songs, s => s.name == name);
        if (s == null || s == playing)
        {
            Debug.Log("couldnt find audio: " + name + " or this song is playing rn");
            return;
        }

        ending = playing;
        playing = s;

        if (ending != null)
        {
            mixer.SetFloat("EndingVolume", 0);
            ending.source.outputAudioMixerGroup = endGroup;
            StartCoroutine(StartFade(mixer, "EndingVolume", fadeTime, 0)); 
        }

        mixer.SetFloat("PlayingVolume", -80);
        playing.source.outputAudioMixerGroup = playGroup;
        playing.source.Play();

        
        fading = StartCoroutine(StartFade(mixer, "PlayingVolume", fadeTime, 100));
    }

    public void muteSong()
    {
        if(fading == null) mixer.SetFloat("PlayingVolume", -80);
        muted = true;
    }


    private void OnPresent(PlayingCardScript card)
    { 
        if(fading == null && muted) 
        { 
            mixer.SetFloat("PlayingVolume", 0);
        }
    }


    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfx, s => s.name == name);
        if (s == null) 
        {
            Debug.Log("couldnt find audio: " + name);
            return;
        }
            
        s.source.Play();
    }


    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        yield break;
    }
}

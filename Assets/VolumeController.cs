using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] Slider master, music, sfx;
    [SerializeField] AudioMixer mixer;
    public void setMasterVolume(){
        if (master.value > master.minValue) mixer.SetFloat("masterVolume", master.value);
        else mixer.SetFloat("masterVolume", -60);
    }
    public void setMusicVolume(){
        if (music.value > music.minValue) mixer.SetFloat("musicVolume", music.value);
        else mixer.SetFloat("musicVolume", -60);
    }
    public void setSFXVolume(){
        if (sfx.value > sfx.minValue) mixer.SetFloat("sfxVolume", sfx.value);
        else mixer.SetFloat("sfxVolume", -60);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteOnAwake : MonoBehaviour
{
    void Start()
    {
        Debug.Log("mute");
        AudioManager.instance.MuteSong();
    }
}

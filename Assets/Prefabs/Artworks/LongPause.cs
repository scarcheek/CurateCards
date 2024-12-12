using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteOnAwake : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Long pasue");
        AudioManager.instance.muteSong();
    }
}

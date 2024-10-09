using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConfigManagerScript : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] public float CardPanSpeed = 5f;
    [SerializeField] public KeyCode QuitGame = KeyCode.Escape;
    [SerializeField] public KeyCode StopVisitorAnimation = KeyCode.Tab;
    [SerializeField] public KeyCode DrawCard = KeyCode.Space;
    [Header("Accessibility")]
    [SerializeField] public Color posEffectColor = Color.yellow;
    [SerializeField] public Color negativeColor = Color.blue;
    [SerializeField] public Color positiveColor = Color.green;



    public static ConfigManagerScript instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI availableCoinsText;
    [SerializeField] private TextMeshProUGUI AttackCounterText;
    [SerializeField] private TextMeshProUGUI DefenceCounterText;
    [SerializeField] private TextMeshProUGUI VirusCounterText;
    [SerializeField] private float StartingCoinAmount = 500;
    [SerializeField] private GameObject Shop;
    [SerializeField] private GameObject CardZones;
    [Range(0f, 1f)][SerializeField] private float initialScoreFactor;
    public int activeAttackCounters = 0;
    public int activeDefenceCounters = 0;
    public int activeVirusCounters = 0;
    public static GameStateManager instance;
    public static float AttackCounterChange = 1.2f;
    public static float DefenceCounterChange = 0.83f;
    public static float AvailableCoins;

    private Color defaultCoinColor;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.EmitStartDay();
    }

    private void Awake()
    {
        instance = this;

        defaultCoinColor = availableCoinsText.color;

        EventManager.AddCounter += OnAddCounter;
        EventManager.RemoveCounter += OnRemoveCounter;
        EventManager.StartShopping += StartShopping;
        EventManager.StartDay += OnStartDay;
    }

    private void OnStartDay()
    {
        EventManager.EmitStartTurn();
        AvailableCoins += StartingCoinAmount;
        availableCoinsText.text = AvailableCoins.ToString();
        UpdateCounterText();
    }

    private void StartShopping()
    {
        activeAttackCounters = 0;
        activeDefenceCounters = 0;
        activeVirusCounters = 0;
        UpdateCounterText();
    }


    /// <summary>
    /// On Card Add for the first time: costBefore = 0, costAfter = ex. 20
    /// 100 += 0 - 20 -> 80
    /// On card discount: costbefore = 20, costAfter = ex. 10
    /// 80 += 20 - 10
    /// </summary>
    /// <param name="costBefore">The cost before the change</param>
    /// <param name="costAfter">The cost after the change</param>
    public void UpdateAvailableCoins(float costBefore, float costAfter)
    {
        AvailableCoins += costBefore - costAfter;
        availableCoinsText.text = AvailableCoins.ToString();

        if (AvailableCoins >= 0)
        {
            availableCoinsText.color = defaultCoinColor;
        }
        else
        {
            availableCoinsText.color = Color.blue;
        }
    }

    #region counters
    private void ReduceCountersByOne()
    {
        EventManager.EmitRemoveCounter(Counter.attack, Counter.defence, Counter.virus);
    }
    private void OnAddCounter(Counter[] counters)
    {
        foreach (Counter counter in counters)
        {
            switch (counter)
            {
                case Counter.attack:
                    activeAttackCounters++;
                    break;
                case Counter.defence:
                    activeDefenceCounters++;
                    break;
                case Counter.virus:
                    activeVirusCounters++;
                    break;
            }
        }
        UpdateCounterText();
    }
    private void OnRemoveCounter(Counter[] counters)
    {
        foreach (Counter counter in counters)
        {
            switch (counter)
            {
                case Counter.attack:
                    activeAttackCounters--;
                    break;
                case Counter.defence:
                    activeDefenceCounters--;
                    break;
                case Counter.virus:
                    activeVirusCounters--;
                    break;
            }
        }
        activeAttackCounters = math.max(0, activeAttackCounters);
        activeDefenceCounters = math.max(0, activeDefenceCounters);
        activeVirusCounters = math.max(0, activeVirusCounters);
        UpdateCounterText();
    }
        
    private void UpdateCounterText()
    {
        SetSpecificCounterText(AttackCounterText, activeAttackCounters);
        SetSpecificCounterText(DefenceCounterText, activeDefenceCounters);
        SetSpecificCounterText(VirusCounterText, activeVirusCounters);

    }
    private void SetSpecificCounterText(TextMeshProUGUI text, int counterAmount)
    {
        if (counterAmount == 0)
        {
            text.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            text.transform.parent.gameObject.SetActive(true);
            text.text = counterAmount.ToString();
        }
    }
    #endregion
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

}

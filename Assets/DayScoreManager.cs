using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DayScoreManager : MonoBehaviour 
{
    [SerializeField] private List<float> scoresToAchieve = new();
    [SerializeField] private TextMeshProUGUI scoreText;
    public int CurrentDay { get; private set; }
    private Color defaultScoreTextColor;
    private float CurrentScore = 0;


    public static DayScoreManager instance;
    private void Start()
    {
        instance = this;
        CurrentDay = 0;
        defaultScoreTextColor = scoreText.color;
        EventManager.CurationDone += OnCurationDone;
        EventManager.AddBaseValueToGamestate += addBaseValue;
        SetScoreText();
    }

    private void SetScoreText()
    {
        StringBuilder sb = new StringBuilder(CurrentScore.ToString());
        sb.Append(" / ");
        sb.Append(GetTodaysScoreToAchieve().ToString());
        scoreText.text = sb.ToString();
        if (CurrentScore > GetTodaysScoreToAchieve())
        {
            scoreText.color = Color.yellow;
        }
        else
        {
            scoreText.color = defaultScoreTextColor;
        }
    }

    public float GetTodaysScoreToAchieve() => scoresToAchieve[CurrentDay];

    private void OnCurationDone()
    {
        if (CurrentScore >= GetTodaysScoreToAchieve())
        {
            EventManager.EmitDayComplete();
        } 
        else
        {
            EventManager.EmitStartTurn();
        }
    }

    public void addBaseValue(float baseVal)
    {
        //TODO: Display the score somewhere in the UI
        CurrentScore += baseVal;
        SetScoreText();
        Debug.Log("Current Score: " + CurrentScore);
    }
}
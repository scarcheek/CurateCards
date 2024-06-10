using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreVisualizerScript : MonoBehaviour
{
    private TextMeshPro scoreText;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshPro>();
        anim = GetComponent<Animator>();
    }

    public void showScore(double score, float motivationChange)
    {
        scoreText.text = "";
        if (score >= 0) scoreText.text = "+";
        scoreText.text += score.ToString();

        if (motivationChange >= 0) scoreText.color = Color.green;
        else scoreText.color = Color.blue;

        anim.SetTrigger("showScore");
    }

}

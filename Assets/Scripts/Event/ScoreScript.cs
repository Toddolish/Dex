using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    [Header("SCORE")]
    public int scoreCount = 0;
    Text scoreText;

    [Header("MULTIPLIER")]
    Text multiplierText;
    public int multiplierCount;
    
    void Start()
    {
        scoreText = GameObject.Find("ScoreCount").GetComponent<Text>();
        multiplierText = GameObject.Find("MultiCount").GetComponent<Text>();
        multiplierCount = 1;
    }

    void Update()
    {
        multiplierText.text = multiplierCount.ToString("F0");
        scoreText.text = scoreCount.ToString("F0");
    }
}

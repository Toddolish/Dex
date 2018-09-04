using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    [Header("SCORE")]
    public int scoreCount = 0;
    public Text scoreText;

    void Start()
    {
        scoreText = GameObject.Find("ScoreCount").GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = scoreCount.ToString("F0");
    }
}

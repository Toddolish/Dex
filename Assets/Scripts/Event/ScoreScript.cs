using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    [Header("SCORE")]
    public int scoreCount = 0;
    int highScore;
    Text scoreText;
    Text highscoreText;

    [Header("MULTIPLIER")]
    Text multiplierText;
    public int multiplierCount;
    
    void Start()
    {
        scoreText = GameObject.Find("ScoreCount").GetComponent<Text>();
        multiplierText = GameObject.Find("MultiCount").GetComponent<Text>();
        highscoreText = GameObject.Find("HighScore").GetComponent<Text>();
        highscoreText.text = PlayerPrefs.GetInt("highScore", highScore).ToString("C0");
        multiplierCount = 1;
    }

    void Update()
    {
        if(scoreCount > PlayerPrefs.GetInt("highScore"))
        {
            highScore = scoreCount;
            PlayerPrefs.SetInt("highScore", highScore);
            highscoreText.text = PlayerPrefs.GetInt("highScore", highScore).ToString("C0");
        }
        multiplierText.text = multiplierCount.ToString("F0");
        scoreText.text = scoreCount.ToString("C0");
        
    }
    /* multiplierText.text = multiplierCount.ToString("F0");
        
        int hundread = (int)(scoreCount / 100);
        int thousand = (int)(scoreCount / 1000);
        int million = (int)(scoreCount / 100000);

        string scoreTextCount = string.Format("{000,} + {000,} + {000}", million,thousand,hundread);
        scoreText.text = scoreTextCount;*/
}

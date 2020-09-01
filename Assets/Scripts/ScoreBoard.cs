using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{

    int score = 0;
    Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    public void ScoreHit(int scoreIncrease)
    {
        score += scoreIncrease;
        if(score > 99999999) { score = 99999999; } // Set max score
        scoreText.text = score.ToString();
    }
    
}

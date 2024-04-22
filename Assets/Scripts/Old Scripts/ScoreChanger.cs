using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Score Changer Script
 * Outdated. Now handled by Game Manager
 * Place on Score UI to change score. Call IncreasePoints or decreasePoints functions to cahnge score.
 * 
 * Handles:
 * Score, how many points to increase or decrease
 */

public class ScoreChanger : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public int points = 0;
    public int pointsLoss = 0;

    private void Awake()
    {
        //get score text
        scoreText = GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        scoreText.text = "Score: " + points;
    }

    //randomly add points to the score
    public void incrementPoints()
    {
        int tempPoints = Random.Range(-1, 10);
        if(tempPoints == 0)
        {
            tempPoints = 1;
        }
        points += tempPoints;
    }

    //decrease points by specified amount
    public void decreasePoints()
    {
        points = points - pointsLoss;
    }
}

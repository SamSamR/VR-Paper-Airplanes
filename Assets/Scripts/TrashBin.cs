using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* TrashBin Script
 * Place on Trashbin(s) (w/trigger). Tells GameManager to change score when plain enters the bin. 
 * Changes specified points UI based on points assigned.
 * 
 * Handles:
 * How many points are earned, changes points UI/text, gets GameManager, tells GameManager to change points
 */

public class TrashBin : MonoBehaviour
{
    //how many points player earns
    public int points = 10;

    //Specified Points UI
    public TextMeshProUGUI scoreText;

    [SerializeField]
    private GameManager gameManager;


    private void Awake()
    {
        //set score text to points
        scoreText.text = points.ToString();

        //get game manager
        gameManager = FindObjectOfType<GameManager>();
    }

    //if plane enters trigger, tell Game Manger to increase score by points assigned
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Plane")
        {
            //use gamemanager to increase score
            gameManager.BinHit(points);
        }
    }


}

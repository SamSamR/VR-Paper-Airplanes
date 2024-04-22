using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Changel Level Script
 * Put on Button Object. Tells game manager to go to next level
 */

public class ChangeLevel : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

   void Start()
    {
        //get game manager
        gameManager = FindObjectOfType<GameManager>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("hit button");
            gameManager.ChangeScene();          
        }
    }

}



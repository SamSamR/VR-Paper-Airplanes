using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Game Manager Script
 * Put on GAmeManager object
 * Handles:
 * UI, Players Score, Level Timer, adding points, points lost, Scene Change
 * 
 */

public class GameManager : MonoBehaviour
{
    //score game objects & variables
    public TextMeshProUGUI scoreText;
    public int Score = 0;

    //timer game objects & variables
    public TextMeshProUGUI clock;
    private float realTime;
    private int sec = 0;
    private int min = 0;

    //points lost when plain hits floor
    public int pointsLost = 5;

    //level change objects & variables
    [SerializeField] private int CurrentSceneIndex;
    [SerializeField] private int TotalSceneIndex;
    [SerializeField] private int NextScene;

    private void Awake()
    {
        //Gets the current scenes Index
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex; ;
        //Debug.Log("Current scene index: " + CurrentSceneIndex);

        //Get the Total number of scenes in Build
        TotalSceneIndex = SceneManager.sceneCountInBuildSettings;
        //Debug.Log("Total scene index: " + TotalSceneIndex);

        //Ready next scene to load
        ReadyNextScene();
    }


    // Update is called once per frame
    void Update()
    {
        myTimer();
    }


    //command for when airplane enters a bin, add points
    public void BinHit(int addPoints)
    {
        Score = Score + addPoints;

        //update score ui
        scoreText.text = "Score: " + Score.ToString();

    }


    //command for when airplane hits floor, decrease points
    public void FloorHit()
    {
        Score = Score - pointsLost;

        //update score ui
        scoreText.text = "Score: " + Score.ToString();
    }


    //clock/timer
    public void myTimer()
    {
        realTime += Time.deltaTime;
        sec = (int)realTime;
        clock.text = min + ":" + sec.ToString("00");

        if (realTime >= 60)
        {
            min++;
            realTime = 0;
        }
    }


    //What Scene to load next
    private void ReadyNextScene()
    {
        if (CurrentSceneIndex != (TotalSceneIndex - 1))
        {
            NextScene = CurrentSceneIndex + 1;
        }
        else
        {
            NextScene = 0;
        }

        //Debug.Log("Next Scene: " + NextScene);
    }


    //time to wait before scene loads
    private IEnumerator buttonWait()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(NextScene);
    }


    //command to load next scene
    public void ChangeScene()
    {
        //Debug.Log("Load Next Scene: " + NextScene);
        StartCoroutine(buttonWait()); 
    }
}

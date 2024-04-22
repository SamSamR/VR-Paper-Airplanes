using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Clock Script
 * Outdated. Now handled by Game Manager
 * Place on Clock/Timer UI to change time. Call functions to start/stop timer.
 * 
 * Handles:
 * Timer, if timer should start or stop
 */

public class Clock : MonoBehaviour
{

    public TextMeshProUGUI clock;
    private float realTime;
    private int sec = 0;
    private int min = 0;

    //determines if timer automatically starts 
    public bool start = false;

    private void Awake()
    {
        //gets clock text
        clock = GetComponent<TextMeshProUGUI>();
    }

    //updates the timer
    private void Update()
    {
        //check if timer should start/stop
        if (start)
        {
            //timer math
            realTime += Time.deltaTime;
            sec = (int)realTime;
            clock.text = min + ":" + sec.ToString("00");

            if (realTime >= 60)
            {
                min++;
                realTime = 0;
            }

        }

    }

    //timer start
    public void timerStart()
    {
        start = true;
    }

    //tiemr stop
    public void timerStop()
    {
        start = true;
    }

}

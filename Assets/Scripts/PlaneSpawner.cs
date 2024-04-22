using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Plane Spawner Script
 * Place on Plane Spawner (w/Trigger) to spawn a specified plane prefab. 
 * 
 * Handles:
 * What plane to spawn
 * Spawning the first plane
 * Spawns new planes when there is nothing in the spawn area
 */

public class PlaneSpawner : MonoBehaviour
{
    //plane prefab to use 
    public GameObject plane;
    //spawned plane gamobject
    public GameObject tempPlane;

    //tracks if objects have entered spawn area
    [SerializeField]
    private int objects = 0;


    private void Awake()
    {
        //spawn first plane
        tempPlane = Instantiate(plane, transform.position, Quaternion.identity);
    }


    private void OnTriggerEnter(Collider other)
    {
        //increase object counter if something enters the trigger
        objects++;
    }


    void OnTriggerExit(Collider other)
    {
        //decreses object counter if something leaves the trigger
        objects--;

        //spawn a new plane
        spawnPlain();
    }


    //spawns anew plane if there is no objects in the spawn area
    public void spawnPlain()
    {
        if (objects == 0)
        {
            tempPlane = Instantiate(plane, transform.position, Quaternion.identity);
        }
    }
}

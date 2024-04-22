using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Air Plane Behavior Script
 * Put on airplane prefab. Handles the interactions/behaviours between the plane & specific objects.
 * 
 * Handles:
 * The plane's collision with goals.
 * The plane's collison w/ the floor, reduce points, & stops movement.
 * The plane's collision w/walls, it sticks to walls.
 * The plane's collsion with a platform. Becomes untagged so planes don't trigger unnessary collision w/goals.
 * Checks if plane is grabbed or not.
 */


public class AirplaneBehaviour : MonoBehaviour
{
    public Rigidbody RB;
    public bool grabbed = false;

    [SerializeField]
    private GameManager gameManager;


    private void Awake()
    {
        //get rigid body & game manager
        RB = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }


    public void isGrabbed()
    {
        grabbed = true;
        //reset contraints by unfreezing then Re freezes contraints when picked up
        RB.constraints = RigidbodyConstraints.None;
        RB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

    }

    public void notGrabbed()
    {
        grabbed = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        //plane collides with a goal unfreeze rotation contrainsts to prevent physics wonkyness
        if (other.gameObject.tag == "Goal")
        {
            this.RB.freezeRotation = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //plane collides with floor, set velocity to zero, Freeze & unfreeze to prevent physics wonkyness
        if (collision.gameObject.tag == "Floor")
        {
            RB.velocity = Vector3.zero;
            RB.constraints = RigidbodyConstraints.FreezeAll;
            RB.constraints = RigidbodyConstraints.None;

            //tell game manager to reduce points
            gameManager.FloorHit();

        }

        //plane collides with wall, set velocity to zero, Freeze contraints so it sticks to wall
        if (collision.gameObject.tag == "Wall")
        {
            RB.velocity = Vector3.zero;
            RB.constraints = RigidbodyConstraints.FreezeAll;
        }

        //plane collides with platform, untag so plane can't re-trigger goals on collision by accident
        if (collision.gameObject.tag == "Platform" )
        {
            this.gameObject.tag = "Untagged";
        }

    }
}

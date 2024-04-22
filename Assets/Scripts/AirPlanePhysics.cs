using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Air Plane Physics Script
 * Put on airplane prefab to adjust its physics & physics interactions.
 * Can adjust plains flight time & gravity to make it as realistic or floaty as wanted.
 * 
 * Handles:
 * The planes Phyics: Time in air, gravity applied while in flight, gravity applied while falling.
 * Checks if plane has been grabbed, has been thrown, launched/in air, or crashed.
 */

public class AirPlanePhysics : MonoBehaviour
{
    public Rigidbody RB;
    
    public float flightTime = 2f;     //time airplaine should be in the air (seconds)
    public float flightGrav = 2.45f;    //force applied to airplane while in flight
    public float crashGrav = 15f;       //force applied to airplane while falling

    [SerializeField]
    private bool grabbed = false;
    [SerializeField]
    private bool thrown = false;
    [SerializeField]
    private bool planeLaunch = true;    //true while in hand or in air, false when it starts to fall
    [SerializeField]
    private bool hasCrashed = false;


    private void Awake()
    {
        //get rigidbody & set use Gravity to false so it uses assigned gravity peramiters
        RB = GetComponent<Rigidbody>();
        RB.useGravity = false;
    }


    void FixedUpdate() //changed from update
    {
        //if the plane is starng to fall
        if (!planeLaunch)
        {
            //if plane has not crashed
            if (!hasCrashed)
            {
                //apply flight gravity
                RB.AddForce(new Vector3(0, -flightGrav, 0), ForceMode.Acceleration);
            }
            //if plane has crashed apply crash gravity
            else
            {
                RB.AddForce(new Vector3(0, -crashGrav, 0), ForceMode.Acceleration);
            }
        }
    }


    //called when player picks up airplane (XR Grab Interactable, Select Entered)
    public void isGrabbed()
    {
        //resets plains variables so it can be relaunched after picking it up
        grabbed = true;
        planeLaunch = true;
        hasCrashed = false;

        RB.useGravity = false; 
    }

    //called when player lets go of airplane (XR Grab Interactable, Select Exited)
    public void notGrabbed()
    {
        //checks if plane was previously grabbed
        if(grabbed == true)
        {
            //plane has been thrown
            thrown = true;
        }

        grabbed = false;
    }


    //enables gravity after specified flight time
    private IEnumerator enableGravity()
    {
        thrown = false; //no longer in hand

        yield return new WaitForSeconds(flightTime);

        planeLaunch = false;    //plane should start to fall
    }

    
    private void OnTriggerExit(Collider other)
    {
        //plane is in Hand, not grabbed & leaves players hand (w/trigger)
        if (this.thrown == true && this.grabbed == false && other.tag == "Player")
        {
            //Debug.Log("Thrown plane");
            //start flight time
            StartCoroutine(enableGravity());
        }

    }

    
    private void OnCollisionEnter(Collision collision)
    {
        //plane isn't grabbed collides with anything that isn't the player
        if (this.grabbed == false && collision.gameObject.tag != "Player")
        {
            //has crashed, use regular gravity
            hasCrashed = true;
            RB.useGravity = true;
        }
    }


}

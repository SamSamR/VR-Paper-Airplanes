using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Rotation Script
 * Place on Object/parent object you wish to rotate on any axis.
 * Doesn't use physics to rotate (should change later)
 * Call function to change axis direction
 * 
 * Handles:
 * The roation Axis, rigid body & speed of rotation. 
 * Make speed negative to rotate in oposite direction.
 */

public class Rotation : MonoBehaviour
{
    //select direction to rotate Object
    public enum Direction 
    { 
        X, Y, Z
    };
    public Direction RotationDirection;

    private Vector3 axis;

    
    public float RotationSpeed = 10f;

    [SerializeField]
    private Rigidbody myRigBody;

    [SerializeField]
    private Transform myTransform;


    private void Awake()
    {
        //get transform & rigidbody
        myTransform = this.transform;
        myRigBody = GetComponent<Rigidbody>();

        //if no rigid body exists add one
        if(myRigBody == null)
        {
            Debug.Log("No RigidBody on " + this.gameObject.name + " Adding one & freezing its posiiton.");

            this.gameObject.AddComponent<Rigidbody>();
            myRigBody = GetComponent<Rigidbody>();

            //freeze its position
            myRigBody.constraints = RigidbodyConstraints.FreezePosition;
        }

        //sets axis for rotaion
        axisSelection(RotationDirection);
        
    }


    //rotation
    void FixedUpdate() 
    {
        axisSelection(RotationDirection);
        transform.RotateAround(myTransform.position, axis, RotationSpeed * Time.deltaTime);
    }


    //function for setting axis Selection
    public void axisSelection(Direction myDirection)
    {
        if(myDirection == Direction.X) 
        {
             axis = Vector3.right;
        }
        else if(myDirection == Direction.Y)
        {
             axis = Vector3.up;
        }
        else if (myDirection == Direction.Z)
        {
             axis = Vector3.forward;
        }

    }

}

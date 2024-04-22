using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildRotation : MonoBehaviour
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
        if (myRigBody == null)
        {
            //Debug.Log("No RigidBody on " + this.gameObject.name + " Adding one & freezing its posiiton.");

            this.gameObject.AddComponent<Rigidbody>();
            myRigBody = GetComponent<Rigidbody>();

            //sets axis for rotaion
            axisSelection(RotationDirection);

            //freeze its position
            myRigBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            myRigBody.isKinematic = true;
            myRigBody.useGravity = false;
            //myRigBody.constraints = RigidbodyConstraints.FreezePositionZ;
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
        if (myDirection == Direction.X)
        {
            axis = Vector3.right;
            myRigBody.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        else if (myDirection == Direction.Y)
        {
            axis = Vector3.up;
        }
        else if (myDirection == Direction.Z)
        {
            axis = Vector3.forward;
            myRigBody.constraints = RigidbodyConstraints.FreezePositionX;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Plane")
        {
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}

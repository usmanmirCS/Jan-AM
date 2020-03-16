using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimHandGrabn : MonoBehaviour
{
    public GameObject collidingObject; // What we're touching

    public GameObject heldObject; // Remember whjat we're holding

    public GameObject prefabForFun; // FOR FUN ONLY

    public Animator handAnimator; // Open close hand

    #region Collision Detection

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() == true) // If this object we touch as a Rigidbody (physics object)
        {
            collidingObject = other.gameObject; // Taking note of what we're touching, so we can grab it on mouse click
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == collidingObject) // Check we exited the "colliding oibject" and not some other object
        {
            collidingObject = null; // "Forget" that object
        }
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        // Grab inputs
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Check ouse input
        {
            if (collidingObject != null) // If collidingObject exists (null = empty)
            {
                Grab();
            }

            handAnimator.SetBool("IsClosed", true); // Close the hand
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(heldObject != null) // If heldObject exists (null = empty)
            {
                Release();
            }

            handAnimator.SetBool("IsClosed", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            BroadcastMessage("Interact");
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            BroadcastMessage("Stop");
        }
        else if(Input.GetKeyDown(KeyCode.Mouse2))
        {
            BroadcastMessage("AltInteract");
        }


        // FOR FUN ONLY
        //if(Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    GameObject newInstance = Instantiate(prefabForFun, transform.position, transform.rotation); // makes a clone of a prefab and gives us a reference to it (i.e. "returns" that new cloned object)

        //    newInstance.GetComponent<Rigidbody>().AddForce(transform.forward * 15f, ForceMode.Impulse);
        //}
    }

    #region  Grab and Release
    
    void Grab()
    {
        //collidingObject.GetComponent<Rigidbody>().isKinematic = true; // Not respond to gravity + forces

        FixedJoint fx = gameObject.AddComponent<FixedJoint>();

        fx.connectedBody = collidingObject.GetComponent<Rigidbody>();

        fx.breakForce = 10000;

        fx.breakTorque = 10000;

        collidingObject.transform.parent = transform; // "transform" is this hand

        heldObject = collidingObject; // To remember what we are holding
    }

    void Release()
    {
        //heldObject.GetComponent<Rigidbody>().isKinematic = false;

        Destroy(GetComponent<FixedJoint>());

        heldObject.transform.parent = null; // unparent

        heldObject = null; // "Forget" what we were just holding 
    }

    private void OnJointBreak(float breakForce)
    {
        heldObject.transform.parent = null;
        heldObject = null;
    }
    #endregion
}

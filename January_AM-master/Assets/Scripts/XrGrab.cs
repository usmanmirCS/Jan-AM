using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Applies to both hands
public class XrGrab : MonoBehaviour
{
    public GameObject collidingObject; // What we're touching

    public GameObject heldObject; // Remember whjat we're holding

    public GameObject prefabForFun; // FOR FUN ONLY

    public Animator handAnimator; // Open close hand

    private Vector3 m_handVelocity;

    private Vector3 m_oldPosition;

    private Vector3 m_handSpin;

    private Vector3 m_oldRotation;

    #region Hand Input
    public string gripAxisInputName; // So can have this script work on either hand

    bool gripIsHeld = false; // A flag to prevent the grab function from happening every frame (false by default)

    [SerializeField]
    private string m_triggerName;

    [SerializeField]
    private bool m_triggerHeld;

    [SerializeField]
    private string m_menuButton;
    #endregion

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
        if (other.gameObject == collidingObject) // Check we exited the "colliding oibject" and not some other object
        {
            collidingObject = null; // "Forget" that object

        }
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        Vector3 tp = transform.position;
        m_handVelocity = tp - m_oldPosition;
        m_oldPosition = tp;

        Vector3 te = transform.eulerAngles;
        m_handSpin = te - m_oldRotation;
        m_oldRotation = te;

        // Grab inputs
        if (Input.GetAxis(gripAxisInputName) > 0.5f && gripIsHeld == false) // Check ouse input
        {
            if (collidingObject != null) // If collidingObject exists (null = empty)
            {
                Grab();
            }

            handAnimator.SetBool("IsClosed", true); // Close the hand

            gripIsHeld = true; // We are now holding the grip, so take note of that
        }
        else if (Input.GetAxis(gripAxisInputName) < 0.5f && gripIsHeld == true) // i.e. if the grip has been released (more 
        {
            if (heldObject != null) // If heldObject exists (null = empty)
            {
                Release();
            }

            handAnimator.SetBool("IsClosed", false);

            gripIsHeld = false; // No longer being held, so take note of that
        }

        if (Input.GetAxis(m_triggerName) > 0.5f && !m_triggerHeld)
        {
            m_triggerHeld = true;
            BroadcastMessage("Interact");
        }
        else if (Input.GetAxis(m_triggerName) < 0.5f && m_triggerHeld)
        {
            m_triggerHeld = false;
            BroadcastMessage("Stop");
        }

        if (Input.GetButtonDown(m_menuButton))
        {
            BroadcastMessage("AltInteract");
        }

        // FOR FUN ONLY
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject newInstance = Instantiate(prefabForFun, tp, transform.rotation); // makes a clone of a prefab and gives us a reference to it (i.e. "returns" that new cloned object)

            newInstance.GetComponent<Rigidbody>().AddForce(transform.forward * 15f, ForceMode.Impulse);
        }
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

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();

        rb.velocity = m_handVelocity * 50 / rb.mass;
        rb.angularVelocity = m_handSpin * 10 / rb.mass;

        heldObject = null; // "Forget" what we were just holding 
    }

    private void OnJointBreak(float breakForce)
    {
        heldObject.transform.SetParent(null);
        heldObject = null;
    }
    #endregion
}
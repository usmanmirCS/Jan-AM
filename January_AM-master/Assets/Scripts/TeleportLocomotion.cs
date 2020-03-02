using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLocomotion : MonoBehaviour
{
    public LineRenderer laserLine;

    public string teleportButtonName; // Define this name in the inspector 

    Vector3 teleportPosition; // Keeps track of the point on the ground we are pointing at

    bool teleportValid;

    public Transform xrRig; // This is what we are moving

    // Update is called once per frame
    void Update()
    {
        laserLine.SetPosition(0, transform.position); // Set the start of the line to be at the hand always

        if(Input.GetButton(teleportButtonName)) // As long as we are holding the button down
        {
            laserLine.enabled = true; // Turn on laser whenever button pressed

            RaycastHit raycastHitInfo; // Stores info aboutr raycast hit e.g. position

            if(Physics.Raycast(transform.position, transform.forward, out raycastHitInfo)) // Determines what hit, and dumps results into raycastHitInfo 
            {
                teleportPosition = raycastHitInfo.point; // Take note of the position that is hit on the ground

                laserLine.SetPosition(1, teleportPosition);

                teleportValid = true; 
            }
            else // Pointing off into infinite space
            {
                laserLine.SetPosition(1, transform.position + transform.forward * 100f); // Cast the laser, say 100meters in front of the hand

                teleportValid = false;
            }
        }
        else if(Input.GetButtonUp(teleportButtonName)) // When the button released, then attampt teleport
        {
            laserLine.enabled = false; // Turn off laser when button is released

            if(teleportValid == true) // Before teleporting, I do want to ensure that the teleportPosition is valid
            {
                Vector3 offset = xrRig.position - Camera.main.transform.position; // Arrow pointing from headset (cam) to teleport pos
                offset.y = 0; // No vert movement!!

                xrRig.transform.position = teleportPosition + offset; // Move the rig
            }
        }
    }
}
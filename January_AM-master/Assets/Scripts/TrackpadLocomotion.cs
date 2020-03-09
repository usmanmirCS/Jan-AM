using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackpadLocomotion : MonoBehaviour
{
    public string verticalAxisName, horizontalAxisName;

    public Transform xrRig;

    public float speed = 2.5f;

    public LayerMask myGroundLayer;

    void Update()
    {
        float trackpadX = Input.GetAxis(horizontalAxisName); // Get the horizontal axis value for use later
        float trackpadY = Input.GetAxis(verticalAxisName); // Get the vert axis value for use later

        Vector3 forward = transform.forward;
        forward.y = 0f; // Now have the fwd direction but no vertical component

        Vector3 right = transform.right;
        right.y = 0f; // Now have the right direction but no vertical component (dont want to move up/down)

        Vector3 direction = forward * trackpadY + right * trackpadX;

        xrRig.position = xrRig.position + direction * Time.deltaTime * speed;

        float height = GetGroundHeight();

        Vector3 rigPosition = xrRig.position; // Store temporarily the current position of the rig
        rigPosition.y = height;
        xrRig.position = rigPosition;
    }

    private float GetGroundHeight() // This is a like a calculator that returns a number (hieght of ground)
    {
        float floorHeight; // Will store the height of floor

        RaycastHit RaycastHitInfo; // Stores info about whatt was hit (position of ground)

        Physics.Raycast(Camera.main.transform.position, Vector3.down, out RaycastHitInfo, Mathf.Infinity, myGroundLayer);

        floorHeight = RaycastHitInfo.point.y;

        return floorHeight;
    }
}
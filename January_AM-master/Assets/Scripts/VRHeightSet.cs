using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SpatialTracking;

public class VRHeightSet : MonoBehaviour
{
    //Set it to floor level
    private void Awake()
    {
        XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);
    }
}
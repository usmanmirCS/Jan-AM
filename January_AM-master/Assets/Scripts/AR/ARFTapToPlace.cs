using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARFTapToPlace : MonoBehaviour
{
    [SerializeField]
    GameObject m_prefabObject;

    [SerializeField]
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_raycastManager;

    private void Awake()
    {
        m_raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if(m_raycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = s_Hits[0].pose;

                Instantiate(m_prefabObject, hitPose.position, hitPose.rotation);
            }
        }
    }
}

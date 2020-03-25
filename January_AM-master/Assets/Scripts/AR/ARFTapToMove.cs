using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARFTapToMove : MonoBehaviour
{
    [SerializeField]
    GameObject m_prefabObject;

    GameObject m_placedObject;

    [SerializeField]
    static List<ARRaycastHit> s_movingHits = new List<ARRaycastHit>();

    ARRaycastManager m_raycastManager;

    private void Awake()
    {
        m_raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if (m_raycastManager.Raycast(touchPosition, s_movingHits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = s_movingHits[0].pose;

                if(!m_placedObject)
                {
                    m_placedObject = Instantiate(m_prefabObject, hitPose.position, hitPose.rotation);
                }
                else
                {
                    m_placedObject.transform.position = hitPose.position;
                    m_placedObject.transform.rotation = hitPose.rotation;
                }
            }
        }
    }
}

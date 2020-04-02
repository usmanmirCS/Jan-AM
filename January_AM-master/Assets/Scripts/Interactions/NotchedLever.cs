using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotchedLever : MonoBehaviour
{

    [SerializeField]
    Transform m_startPos;

    [SerializeField]
    Transform m_endPos;

    [SerializeField]
    List<Transform> m_notches = new List<Transform>();

    [SerializeField]
    float m_notchSpeed = 1f;

    Transform m_closestNotch;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            m_closestNotch = null;

            Vector3 heading = m_endPos.position - m_startPos.position;
            float magnitudeOfHeading = heading.magnitude;
            heading.Normalize();

            Vector3 startToHand = other.transform.position - m_startPos.position;
            float dotProduct = Vector3.Dot(startToHand, heading);

            dotProduct = Mathf.Clamp(dotProduct, 0, magnitudeOfHeading);
            Vector3 spot = m_startPos.position + heading * dotProduct;

            transform.position = spot;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            m_closestNotch = m_startPos;

            foreach(var notch in m_notches)
            {
                if(Vector3.Distance(transform.position, notch.position) < Vector3.Distance(transform.position, m_closestNotch.position))
                {
                    m_closestNotch = notch;
                }
            }
        }
    }

    private void Update()
    {
        if(m_closestNotch)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_closestNotch.position, m_notchSpeed * Time.deltaTime);
        }
    }
}

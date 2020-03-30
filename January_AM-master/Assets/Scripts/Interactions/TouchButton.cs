using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class TouchButton : MonoBehaviour
{
    [SerializeField]
    Transform m_upTransform;
    [SerializeField]
    Transform m_downTransform;
    [SerializeField]
    Transform m_buttonMesh;

    [SerializeField]
    UnityEvent m_buttonPressed;
    [SerializeField]
    UnityEvent m_buttonReleased;

    Vector3 m_startPosition;

    private void OnTriggerEnter(Collider other)
    {
        //m_buttonMesh.position = m_downTransform.position;
        m_buttonPressed.Invoke();

        m_startPosition = other.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        m_buttonMesh.position = m_upTransform.position;
        m_buttonReleased.Invoke();
    }



    private void OnTriggerStay(Collider other)
    {
        float deltaPosition = m_startPosition.z - transform.position.z;

        m_buttonMesh.position = new Vector3(m_buttonMesh.position.x, m_buttonMesh.position.y, deltaPosition);
    }
}

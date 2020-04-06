using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SimPointToGrab : MonoBehaviour
{
    private LineRenderer m_pointingLine;

    private Transform m_targetTransform;


    // Start is called before the first frame update
    void Start()
    {
        m_pointingLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.G))
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit))
            {
                m_pointingLine.enabled = true;
                m_pointingLine.SetPosition(0, transform.position);
                m_pointingLine.SetPosition(1, hit.point);
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if(hit.transform.GetComponent<Rigidbody>())
                    {
                        m_targetTransform = hit.transform;
                        Grab();
                    }
                }
            }
            else
            {
                m_pointingLine.enabled = false;
            }
        }
        else
        {
            m_pointingLine.enabled = false;
        }

        if(Input.GetKeyUp(KeyCode.Mouse0) && m_targetTransform)
        {
            Release();
        }
    }


    void Grab()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.connectedBody = m_targetTransform.GetComponent<Rigidbody>();
        m_targetTransform.SetParent(transform);
    }

    void Release()
    {
        Destroy(GetComponent<FixedJoint>());
        m_targetTransform.SetParent(null);
        m_targetTransform = null;
    }
}

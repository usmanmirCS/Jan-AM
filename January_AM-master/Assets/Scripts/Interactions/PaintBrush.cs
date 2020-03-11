using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBrush : MonoBehaviour
{
    [SerializeField]
    GameObject m_prefabTrail;

    Transform m_spawnTransform;

    GameObject m_currentTrail;

    Renderer m_myRenderer;

    List<GameObject> m_trailsDrawn = new List<GameObject>();

    private void Awake()
    {
        m_spawnTransform = transform;
        m_myRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Paint")
        {
            Material paintMaterial = other.GetComponent<Renderer>().material;
            m_prefabTrail.GetComponent<Renderer>().material = paintMaterial;
            m_myRenderer.material = paintMaterial;
        }
    }

    //Broadcasted by Grab scripts
    private void Interact()
    {
        m_currentTrail = Instantiate(m_prefabTrail, m_spawnTransform.position, m_spawnTransform.rotation, m_spawnTransform);
        m_trailsDrawn.Add(m_currentTrail);
    }

    private void Stop()
    {
        m_currentTrail.transform.SetParent(null);
    }

    private void AltInteract()
    {
        if(m_trailsDrawn.Count > 0)
        {
            GameObject lineToBeDeleted = m_trailsDrawn[m_trailsDrawn.Count - 1];
            m_trailsDrawn.Remove(lineToBeDeleted);
            Destroy(lineToBeDeleted);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightn : MonoBehaviour
{
    private Light m_myLight;

    private void Awake()
    {
        m_myLight = GetComponentInChildren<Light>();
    }

    //This receives a message from the SimHandGrabn script
    void Interact()
    {
        m_myLight.enabled = !m_myLight.enabled;
    }
}

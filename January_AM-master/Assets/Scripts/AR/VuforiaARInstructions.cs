using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuforiaARInstructions : MonoBehaviour
{
    [SerializeField]
    private GameObject m_mainMenuCanvas;
    [SerializeField]
    private GameObject m_thisCanvas;

    [SerializeField]
    private List<GameObject> m_steps = new List<GameObject>();

    private int m_currentStep = 0;

    public void StepsController()
    {
        if(m_currentStep < m_steps.Count - 1)
        {
            m_steps[m_currentStep].SetActive(false);
            m_currentStep++;
            m_steps[m_currentStep].SetActive(true);
        }
        else if(m_currentStep == m_steps.Count - 1)
        {
            m_steps[m_currentStep].SetActive(false);
            m_currentStep = 0;
            m_steps[m_currentStep].SetActive(true);
            m_mainMenuCanvas.SetActive(true);
            m_thisCanvas.SetActive(false);
        }
    }
}

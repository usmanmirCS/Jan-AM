using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;       //to use Actions
using System.Linq;  //to use ToArray
using UnityEngine.Windows.Speech;   //To use speech Recognition

public class VoiceCommander : MonoBehaviour
{

    private Dictionary<string, Action> m_keyWordActions = new Dictionary<string, Action>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

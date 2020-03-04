using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;       //to use Actions
using System.Linq;  //to use ToArray
using UnityEngine.Windows.Speech;   //To use speech Recognition

public class VoiceCommander : MonoBehaviour
{
    private Dictionary<string, Action> m_keyWordActions = new Dictionary<string, Action>();

    private KeywordRecognizer m_keywordRecognizer;

    private void Awake()
    {
        m_keyWordActions.Add("slap dale", SlapDale); //You phrase and then the function you want to call
        m_keyWordActions.Add("Ra za na ba do a", SlapDale);
        m_keyWordActions.Add("Spawn dale", SpawnDale);
        m_keyWordActions.Add("Spawn cube", SpawnCube);
        m_keyWordActions.Add("Spawn box", SpawnCube);

        m_keywordRecognizer = new KeywordRecognizer(m_keyWordActions.Keys.ToArray());
        m_keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        m_keywordRecognizer.Start();
    }

    void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        m_keyWordActions[args.text].Invoke();
    }
    
    //The functions you call
    void SlapDale()
    {
        Debug.Log("How dare you! Dale is so cool! Stop being so mean!");
    }

    [SerializeField]
    private GameObject m_prefabDale;

    [SerializeField]
    private Transform m_SpawnPosition;

    void SpawnDale()
    {
        Instantiate(m_prefabDale, m_SpawnPosition.position, m_SpawnPosition.rotation);
    }

    [SerializeField]
    private GameObject m_cube;

    void SpawnCube()
    {
        Instantiate(m_cube, m_SpawnPosition.position, m_SpawnPosition.rotation);
    }
}

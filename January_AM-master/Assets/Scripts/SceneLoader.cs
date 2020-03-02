using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName; // Be the name of the scene file that we want load

    public void LoadTheScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
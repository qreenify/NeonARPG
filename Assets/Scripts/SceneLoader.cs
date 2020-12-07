using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public bool addative;
    public bool onAwake = true;
    private void Awake()
    {
        if (onAwake)
            LoadScene();
    }
    public void LoadScene()
    {
        if (addative)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        else
            SceneManager.LoadScene(sceneName);
    }
}

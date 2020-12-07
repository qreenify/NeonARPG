using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public LoadSceneMode mode = LoadSceneMode.Additive;
    public bool onAwake = true;
    private void Awake()
    {
        if (onAwake)
            LoadScene();
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName, mode);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddScene : MonoBehaviour
{
    public string sceneName;
    private void Awake()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Revive : MonoBehaviour
{
    public int sceneIndex;
    public Vector3 fixedLocation;
    public Mover mover;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetMover()
    {
        mover = FindObjectOfType<Mover>();
    }

    [ContextMenu("Revive At Fixed Location")]
    void ReviveFixedLocation()
    {
        if (SceneManager.GetActiveScene().buildIndex == sceneIndex)
        {
            mover.transform.position = fixedLocation;
            mover.GetComponent<Health>().Revive();
        }
        else
            StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SetMover();
        mover.navMeshAgent.enabled = false;
        mover.transform.position = fixedLocation;
        mover.GetComponent<Health>().Revive();
        mover.navMeshAgent.enabled = true;
    }
}

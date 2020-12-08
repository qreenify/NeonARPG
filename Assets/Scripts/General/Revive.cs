using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Revive : MonoBehaviour
{
    public int sceneIndex;
    public Vector3 fixedLocation;
    public PlayerController playerController;
    public static Revive revive;

    private void Awake()
    {
        if (revive != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            SetMover();
            revive = this;
        }
    }

    public void SetMover()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    [ContextMenu("Revive At Fixed Location")]
    public void ReviveFixedLocation()
    {
        if (SceneManager.GetActiveScene().buildIndex == sceneIndex)
        {
            playerController.transform.position = fixedLocation;
            playerController.GetComponent<Health>().Revive();
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
        playerController.navMeshAgent.enabled = false;
        playerController.transform.position = fixedLocation;
        playerController.GetComponent<Health>().Revive();
        playerController.navMeshAgent.enabled = true;
    }
}

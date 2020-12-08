using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Revive : MonoBehaviour
{
    public int sceneIndex;
    public Vector3 currentRespawnPoint;
    private Vector3 _fixedLocation;
    private int _fixedSceneIndex;
    public PlayerController playerController;
    private static Revive _revive;

    private void Awake()
    {
        if (_revive != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            SetMover();
            _fixedLocation = currentRespawnPoint;
            _fixedSceneIndex = sceneIndex;
            _revive = this;
        }
    }

    public void SetMover()
    {
        playerController = PlayerController.playerController;
    }

    [ContextMenu("Revive At Fixed Location")]
    public void ReviveFixedLocation()
    {
        if (SceneManager.GetActiveScene().buildIndex == sceneIndex)
        {
            playerController.transform.position = currentRespawnPoint;
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
        playerController.navMeshAgent.enabled = false;
        playerController.transform.position = currentRespawnPoint;
        playerController.GetComponent<Health>().Revive();
        playerController.navMeshAgent.enabled = true;
    }

    [ContextMenu("SaveRespawnPoint")]
    public void SaveRespawnPoint()
    {
        currentRespawnPoint = FindObjectOfType<PlayerController>().transform.position;
    }
}

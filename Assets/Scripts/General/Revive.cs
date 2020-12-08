using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Revive : MonoBehaviour
{
    public string scene;
    public Vector3 currentRespawnPoint;
    private static Revive _revive;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_revive != null)
        {
            scene = _revive.scene;
            currentRespawnPoint = _revive.currentRespawnPoint;
            Destroy(_revive.gameObject);
            
            ReviveFixedLocation();
        }
            
        _revive = this;
    }

    [ContextMenu("Revive At Fixed Location")]
    public void ReviveFixedLocation()
    {
        if (SceneManager.GetActiveScene().name == scene)
        {
            PlayerController.playerController.transform.position = currentRespawnPoint;
            PlayerController.playerController.GetComponent<Health>().Revive();
        }
        else
            SceneManager.LoadScene(scene);
    }
    
    public void ReviveCurrentLocation()
    {
        PlayerController.playerController.GetComponent<Health>().Revive();
    }

    public void SetRespawnPoint()
    {
        currentRespawnPoint = PlayerController.playerController.transform.position;
        scene = SceneManager.GetActiveScene().name;
    }

    [ContextMenu("SaveRespawnPoint")]
    public void SaveRespawnPoint()
    {
        currentRespawnPoint = FindObjectOfType<PlayerController>().transform.position;
        scene = SceneManager.GetActiveScene().name;
    }
}

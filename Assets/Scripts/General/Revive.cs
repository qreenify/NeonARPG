using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Revive : MonoBehaviour
{
    public string scene = "Hub";
    public Vector3 currentRespawnPoint;
    private static Revive _revive;
    private static bool _revived;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_revive != null)
        {
            scene = _revive.scene;
            currentRespawnPoint = _revive.currentRespawnPoint;
            Destroy(_revive.gameObject);
            if (_revived)
            {
                ReviveFixedLocation();
                _revived = false;
            }
        }
            
        _revive = this;
    }

    [ContextMenu("Revive At Fixed Location")]
    public void ReviveFixedLocation()
    {
        if (LevelFader.levelFader != null)
        {
            LevelFader.levelFader.Fade();
            StartCoroutine(ReviveFixedDelayed(LevelFader.FadeOutTime));
        }
        else
        {
            if (SceneManager.GetActiveScene().name == scene)
            {
                PlayerController.playerController.transform.position = currentRespawnPoint;
                PlayerController.playerController.GetComponent<Health>().Revive();
            }
            else
            {
                _revived = true;
                SceneManager.LoadScene(scene);
            }
        }
    }

    IEnumerator ReviveFixedDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (SceneManager.GetActiveScene().name == scene)
        {
            PlayerController.playerController.transform.position = currentRespawnPoint;
            PlayerController.playerController.GetComponent<Health>().Revive();
        }
        else
        {
            _revived = true;
            SceneManager.LoadScene(scene);
        }
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

﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Portal otherPortal;
    public Vector3 offset;
    public bool autoAdd = true;
    [Header("For Switching Scenes")]
    public Vector3 position;
    public string portalName;

    [FMODUnity.EventRef]
    public string soundStart;
    [FMODUnity.EventRef]
    public string soundEnd;
    float savedTime;
    public float warmUp = 2;
    bool triggeringJump = false;
    public bool toBeDestroyed;
    public void Start()
    {
        if(autoAdd && otherPortal != null && gameObject.TryGetComponent<PlayerEnter>(out PlayerEnter playerEnter))
        {
            playerEnter.playerEnterEvent.AddListener(delegate { TeleportToLocation(PlayerController.playerController); });
        }
    }
    public void LoadScene(string sceneName)
    {
        if (LevelFader.levelFader != null)
        {
            LevelFader.levelFader.Fade();
        }
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        PlayerController.playerController.navMeshAgent.enabled = false;
        PlayerPrefs.SetInt(portalName + "_state", 1);
        toBeDestroyed = true;
        StartCoroutine(LoadSceneDelayed(LevelFader.FadeOutTime, sceneName));
        //StartCoroutine(LoadAsync(sceneName));
    }

    public void TeleportToLocation(PlayerController controller)
    {
        if (otherPortal == null)
        {
            Debug.LogError("OtherPortal is not assigned either assign it or don't call the method TeleportToLocation(PlayerController controller)");
            return;
        }
        if (LevelFader.levelFader != null)
        {
            LevelFader.levelFader.Fade();
        }

        StartCoroutine(TeleportDelay(LevelFader.FadeOutTime));
    }

    private void Update()
    {
        if(triggeringJump && Time.time - savedTime > warmUp)
        {
            triggeringJump = false;
            GlobalSoundPlayer.globalSoundPlayer.PlaySound(soundEnd);
            if(toBeDestroyed) Destroy(gameObject);
        }
    }

    public void TeleportToLocation(Vector3 position)
    {
        //Debug.Log("Playing:" + soundStart);
        GlobalSoundPlayer.globalSoundPlayer.PlaySound(soundStart);
        triggeringJump = true;
        savedTime = Time.time;
        if (position == Vector3.zero) return;
        PlayerController.playerController.navMeshAgent.destination = position;
        PlayerController.playerController.navMeshAgent.enabled = false;
        PlayerController.playerController.transform.position = position;
        PlayerController.playerController.navMeshAgent.enabled = true;
    }

    IEnumerator LoadSceneDelayed(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
        TeleportToLocation(position);
    }

    IEnumerator TeleportDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        var position = otherPortal.transform.position + otherPortal.offset;
        TeleportToLocation(position);
    }

    IEnumerator LoadAsync(string sceneName)
    {
        PlayerController.playerController.navMeshAgent.enabled = false;
        var wait = SceneManager.LoadSceneAsync(sceneName);
        while (!wait.isDone)
        {
            yield return null;
        }
        TeleportToLocation(position);
        Destroy(gameObject);
    }
    public void OnDrawGizmos()
    {
        if (otherPortal == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, otherPortal.transform.position);
    }
}
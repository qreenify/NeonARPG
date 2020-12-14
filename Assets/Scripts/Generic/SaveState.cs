using System;
using System.Linq;
using UnityEngine;

public class SaveState : MonoBehaviour
{
    private int Collected
    { 
        get => PlayerPrefs.GetInt($"state_{GetInstanceID()}", 0);
        set => PlayerPrefs.SetInt($"state_{GetInstanceID()}", value);
    }

    private void Awake()
    {
        Saved();
    }

    private void Saved()
    {
        if (Collected == (int) CollectedState.Collected)
        {
            SetActive(false);
        }
    }

    private void SetActive(bool active)
    {
        var renderers = GetComponents<Renderer>();
        var colliders = GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = active;
        }
        foreach (var renderer in renderers)
        {
            renderer.enabled = active;
        }
    }

    public void SetCollected()
    {
        Collected = (int) CollectedState.Collected;
    }
    
    private enum CollectedState
    {
        NotCollected,
        Collected
    }
}

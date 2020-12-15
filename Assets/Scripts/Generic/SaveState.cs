using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveState : MonoBehaviour
{
    [SerializeField] private string id = Guid.NewGuid().ToString();
    private static List<SaveState> _savedStates = new List<SaveState>();
    private int Collected
    { 
        get => PlayerPrefs.GetInt($"state_{id}", 0);
        set => PlayerPrefs.SetInt($"state_{id}", value);
    }

    private void Awake()
    {
        _savedStates.Add(this);
        Saved();
    }

    private void Saved()
    {
        if (Collected == (int) CollectedState.Collected)
        {
            SetActive(false);
        }
    }

    public void SetActive(bool active)
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

    public static void RemoveAllSavedStates()
    {
        foreach (var states in _savedStates)
        {
            states.SetCollected(CollectedState.NotCollected);
        }
    }

    public void SetCollected(bool state) => Collected = state ? 1 : 0;
    private void SetCollected(CollectedState state) => Collected = (int) state;
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (gameObject.scene.name == null) return;
        if (id == "")
            id = Guid.NewGuid().ToString();
    }

    [ContextMenu("ResetID")]
    private void ResetID()
    {
        id = "";
    }
#endif
}

public enum CollectedState
{
    NotCollected,
    Collected
}

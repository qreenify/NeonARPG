using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Linq;

public class SaveState : MonoBehaviour
{
    [SerializeField] private int id;
    private static List<SaveState> _savedStates = new List<SaveState>();
    private int Collected
    { 
        get => PlayerPrefs.GetInt($"state_{SceneManager.GetActiveScene().name}_{id}", 0);
        set => PlayerPrefs.SetInt($"state_{SceneManager.GetActiveScene().name}_{id}", value);
    }

    private static int GlobalId
    {
        get => GetGlobalId();
        set => SaveGlobalId(value);
    }

    static void SaveGlobalId(int value)
    {
        var path = $"{Application.dataPath}/EditorData/GlobalId.txt";
        if (!File.Exists(path))
        {
            Debug.LogError($"GlobalId File Does Not Exist At {path}");
        }
        File.WriteAllText(path, value.ToString());
    }
    
    static int GetGlobalId()
    {
        var path = $"{Application.dataPath}/EditorData/GlobalId.txt";
        if (!File.Exists(path))
        {
            Debug.LogError($"GlobalId File Does Not Exist At {path}");
        }
        var lines = File.ReadAllLines(path);
        return lines.Select(int.Parse).FirstOrDefault();
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
        if (id == 0)
        {
            id = GlobalId += 1;
        }
    }
#endif

    [ContextMenu("ResetID")]
    private void ResetID()
    {
        id = 0;
    }
}

public enum CollectedState
{
    NotCollected,
    Collected
}

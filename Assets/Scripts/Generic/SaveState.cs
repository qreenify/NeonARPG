using System;
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
            gameObject.SetActive(false);
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

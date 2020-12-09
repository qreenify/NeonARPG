using System;
using UnityEngine;
using UnityEngine.Events;

public class SetTimePlayed : MonoBehaviour
{
    public UnityEvent<string> onTimePlayed;
    public void Awake()
    {
        if (TimePlayed.Initialized == false)
            TimePlayed.Initialize();
    }

    public void SetTimePlayedText()
    {
        onTimePlayed.Invoke(TimePlayed.GetTimePlayed());
    }

    private void OnDestroy()
    {
        TimePlayed.SaveDestroyedTime();
        TimePlayed.SaveTimePlayed();
        Debug.Log(TimePlayed.GetTimePlayed());
    }
}

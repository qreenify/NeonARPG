﻿using UnityEngine;
using UnityEngine.Events;

public class EnterExitMenu : MonoBehaviour
{
    public UnityEvent onToggleActive;
    public void ToggleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        onToggleActive.Invoke();
    }
}

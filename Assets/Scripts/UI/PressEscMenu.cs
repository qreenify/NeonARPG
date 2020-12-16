using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressEscMenu : MonoBehaviour
{
    public KeyCode enterMenuKey;
    public EnterExitMenu enterExitMenu;
    public bool active;
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;


    void Update()
    {
        if (Input.GetKeyDown(enterMenuKey))
        {
            ToggleUI();
        }
    }
    void ToggleUI()
    {
        enterExitMenu.ToggleActive();

        //Make sound
    }
}

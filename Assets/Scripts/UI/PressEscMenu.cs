using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEscMenu : MonoBehaviour
{
    public KeyCode enterMenuKey;
    public EnterExitMenu enterExitMenu;
  
    void Update()
    {
        if (Input.GetKeyDown(enterMenuKey))
        {
            enterExitMenu.ToggleActive();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEscMenu : MonoBehaviour
{
    public KeyCode enterMenuKey;
    public bool MenuActive;
    public GameObject menuObject;
  
    void Update()
    {
        if (Input.GetKeyDown(enterMenuKey) && !MenuActive)
        {
            MenuActive = true;
        }

        else if (Input.GetKeyDown(enterMenuKey) && MenuActive)
        {
            MenuActive = false;

        }
        DisplayMenu();
    }

    public void DisplayMenu()
    {
        if (MenuActive)
        {
            menuObject.SetActive(true);
        }
        else
        {
            menuObject.SetActive(false);
        }
    }
}

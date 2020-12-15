using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
  //  public DebugScript debugscript;

    public void OnEnable()
    {
        Time.timeScale = 0;
       // debugscript.DebugThis(Time.timeScale.ToString());
    }

    public void OnDisable()
    {
        Time.timeScale = 1;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODTest : MonoBehaviour
{
    public StudioEventEmitter soundThingy;
    public bool soundActive;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ToggleSound();
        }
    }
    void ToggleSound()
    {
        soundActive = !soundActive;
        if (soundActive)
        {
            soundThingy.Play();
        }
        else
        {
            soundThingy.Stop();
        }
    }
}

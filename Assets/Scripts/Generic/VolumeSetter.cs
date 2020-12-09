using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class VolumeSetter : MonoBehaviour
{
    [FMODUnity.EventRef] // Use the search function to fill in the path string below
    public string eventPath;
    [Range(0f,2f)]
    public float volume;
    FMOD.Studio.EventInstance music;
    //public StudioEventEmitter studioEventEmitter;
    private void Awake()
    {

        if (eventPath != null)
        {
            
            music = FMODUnity.RuntimeManager.CreateInstance(eventPath);
            //music.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            music.start();
        }
    }
    void Update()
    {
        music.setVolume(volume);
        //music.release();
    }
    private void OnDestroy()
    {
        music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}

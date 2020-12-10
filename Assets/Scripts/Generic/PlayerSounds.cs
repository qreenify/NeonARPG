using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [FMODUnity.EventRef] // Use the search function to fill in the path string below
    public string eventPath;

    [Range(0f, 2f)]
    public float volume;

    [Range(0f, 1f)]
    public float walking;

    FMOD.Studio.EventInstance music;
    //FMOD.Studio.System system;
    //public StudioEventEmitter studioEventEmitter;
    private void Awake()
    {
        //FMOD.Studio.System.create(out system);
        if (eventPath != null)
        {
            //system.setParameterByName("CharacterMoving", walking);
            //music = FMODUnity.RuntimeManager.CreateInstance(eventPath);
            //music.setParameterByName("CharacterMoving", walking);
            //music.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            music.start();
        }
    }
    void Update()
    {
        music.setVolume(volume);
        //FMOD.Studio.
        //music.setProperty(FMOD.Studio.EVENT_PROPERTY 1, 10);
        //music.release();

        //music.getParameterByName("CharacterMoving", out parameter);
        //music.setParameterByName("CharacterMoving", walking);
        //system.setParameterByName("CharacterMoving", walking);
    }
    private void OnDestroy()
    {
        music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
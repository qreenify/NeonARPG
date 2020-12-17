using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [FMODUnity.EventRef] // Use the search function to fill in the path string below
    public string eventPath;
    public bool playOnEnable = true;

    [Range(0f, 2f)]
    public float _volume = 1f;
    public float Volume 
    {
        get => _volume;
        set
        {
            _volume = value;
            music.setVolume(value);
        }
    }

    public string parameterName = "CharacterMoving";
    [Range(0f, 1f)]
    public float _parameterValue = 1;
    public float ParameterValue
    {
        get => _parameterValue;
        set
        {
            _parameterValue = value;
            music.setParameterByName(parameterName, _parameterValue);
        }
    }

    public bool globalSound = false;
    public float range;

    FMOD.Studio.EventInstance music;
    //FMOD.Studio.System system;
    //public StudioEventEmitter studioEventEmitter;
    private void OnEnable()
    {
        //FMOD.Studio.System.create(out system);
        //if (eventPath != null)
        //{
        //system.setParameterByName("CharacterMoving", walking);
        //music = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        //music.setParameterByName("CharacterMoving", walking);
        //music = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        //music.start();
        //if (!GlobalSound)
        //{
        //    music.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, GetComponent<Rigidbody>()));
        //    FMODUnity.RuntimeManager.AttachInstanceToGameObject(music, transform, GetComponent<Rigidbody>());
        //music.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, minDistance);
        //music.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, maxDistance);
        //}
        //}
        if (playOnEnable)
        {
            Play();
        }
    }

    void Update()
    {
        //if(parameterName != "")
        if (!globalSound)
        {
            if (PlayerController.playerController != null)
            {
                //Debug.Log(this + "Shit" +  Vector3.Distance(transform.position, PlayerController.playerController.transform.position));
                if(Vector3.Distance(transform.position, PlayerController.playerController.transform.position) < range)
                {
                    music.setVolume(_volume);
                }
                else
                {
                    music.setVolume(0);
                }
            }
        }

        //music.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        //FMOD.Studio.
        //music.setProperty(FMOD.Studio.EVENT_PROPERTY 1, 10);
        //music.release();

        //music.getParameterByName("CharacterMoving", out parameter);
        //music.setParameterByName("CharacterMoving", walking);
        //system.setParameterByName("CharacterMoving", walking);
    }

    public void Play()
    {
        if (eventPath != null && eventPath != "" && FMODUnity.RuntimeManager.GetEventDescription(eventPath).isValid())
        {
            music = FMODUnity.RuntimeManager.CreateInstance(eventPath);
            music.setVolume(_volume);
            bool is3D;
            FMODUnity.RuntimeManager.GetEventDescription(eventPath).is3D(out is3D);
            if (is3D)
            {
                var rigidBody = GetComponent<Rigidbody>();
                var rigidBody2D = GetComponent<Rigidbody2D>();
                var transform = GetComponent<Transform>();
                if (rigidBody)
                {
                    music.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, rigidBody));
                    FMODUnity.RuntimeManager.AttachInstanceToGameObject(music, transform, rigidBody);
                }
                else
                {
                    music.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, rigidBody2D));
                    FMODUnity.RuntimeManager.AttachInstanceToGameObject(music, transform, rigidBody2D);
                }
            }
            music.start();
        }
    }
    public void Stop()
    {
        music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void OnDisable()
    {
        music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [FMODUnity.EventRef] // Use the search function to fill in the path string below
    public string eventPath;

    [Range(0f, 2f)]
    public float volume = 1f;

    public string parameterName = "CharacterMoving";
    [Range(0f, 1f)]
    public float parameterValue;


    public bool globalSound = false;
    public float range;

    FMOD.Studio.EventInstance music;
    //FMOD.Studio.System system;
    //public StudioEventEmitter studioEventEmitter;
    private void OnEnable()
    {
        //FMOD.Studio.System.create(out system);
        if (eventPath != null)
        {
            //system.setParameterByName("CharacterMoving", walking);
            //music = FMODUnity.RuntimeManager.CreateInstance(eventPath);
            //music.setParameterByName("CharacterMoving", walking);
            music = FMODUnity.RuntimeManager.CreateInstance(eventPath);
            music.start();
            //if (!GlobalSound)
            //{
            //    music.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, GetComponent<Rigidbody>()));
            //    FMODUnity.RuntimeManager.AttachInstanceToGameObject(music, transform, GetComponent<Rigidbody>());
                //music.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, minDistance);
                //music.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, maxDistance);
            //}
        }
    }
    void Update()
    {
        //if(parameterName != "")
        music.setParameterByName("CharacterMoving", parameterValue);
        if (!globalSound)
        {
            if (PlayerController.playerController != null)
            {
                //Debug.Log(this + "Shit" +  Vector3.Distance(transform.position, PlayerController.playerController.transform.position));
                if(Vector3.Distance(transform.position, PlayerController.playerController.transform.position) < range)
                {
                    music.setVolume(volume);
                }
                else
                {
                    music.setVolume(0);
                }
            }
        }
        else
        {
            music.setVolume(volume);
        }

        //music.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        //FMOD.Studio.
        //music.setProperty(FMOD.Studio.EVENT_PROPERTY 1, 10);
        //music.release();

        //music.getParameterByName("CharacterMoving", out parameter);
        //music.setParameterByName("CharacterMoving", walking);
        //system.setParameterByName("CharacterMoving", walking);
    }
    private void OnDisable()
    {
        music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
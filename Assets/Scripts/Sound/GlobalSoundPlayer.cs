using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundPlayer : MonoBehaviour
{
    public static GlobalSoundPlayer globalSoundPlayer;
    public SoundPlayer[] soundPlayers;

    private void Awake()
    {
        if(globalSoundPlayer == null)
        {
            globalSoundPlayer = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void PlaySound(string eventPath)
    {
        foreach (var sound in soundPlayers)
        {
            if(sound.eventPath == eventPath)
            {
                sound.Play();
            }
        }
    }
}
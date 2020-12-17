using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundPlayer : MonoBehaviour
{
    public static GlobalSoundPlayer globalSoundPlayer;
    public List<SoundPlayer> soundPlayers = new List<SoundPlayer>();

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
        foreach (var item in GetComponents<SoundPlayer>())
        {
            soundPlayers.Add(item);
        }
    }
    public void PlaySound(string eventPath)
    {
        foreach (var sound in soundPlayers)
        {
            if(sound.eventPath == eventPath && eventPath != "")
            {
                sound.Play();
                return;
            }
        }
    }
}
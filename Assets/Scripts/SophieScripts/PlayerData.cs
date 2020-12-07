using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int healthdata;
    public float[] position;
    public PlayerData (SaveLoadPlayer player)
    {
        healthdata = player.health;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}

[System.Serializable]

public class SceneData
{
    public int scenedata;

    public SceneData(SwitchScenes scene)
    {
        scenedata = scene.currentScene;
    }
}

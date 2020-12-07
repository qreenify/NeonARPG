using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadPlayer : MonoBehaviour
{
    public float health;

    public void SavePlayer()
    {
        health = GetComponent<Health>().CurrentHealth;
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        health = data.healthdata;
        GetComponent<Health>().CurrentHealth = health;

        //health = data.healthdata;


        Vector3 position;

        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;

    }
    
}

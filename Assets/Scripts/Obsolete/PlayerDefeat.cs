using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerDefeat : MonoBehaviour
{
    public UnityEvent OnDefeat;
    public UnityEvent OnRevive;
    
    public void Defeat(float health)
    {
        if (health > 0)
        {
            return;
        }
        gameObject.SetActive(false);
        OnDefeat.Invoke();
        //TODO: Trigger defeat sound / animation
    }

    public void Revive()
    {
        gameObject.SetActive(true);
        OnRevive.Invoke();
        //TODO: Trigger revive sound / animation
    }



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Defeat(0);
        }
    }
}

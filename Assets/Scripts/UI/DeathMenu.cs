﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<PlayerController>().GetComponent<Health>().onDefeat.AddListener(delegate { gameObject.SetActive(true); });
        gameObject.SetActive(false);
    }

    public void RespawnSetLocation()
    {
        gameObject.SetActive(false);
        FindObjectOfType<Revive>().ReviveFixedLocation();
    }
    
    public void RespawnCurrentLocation()
    {
        gameObject.SetActive(false);
        FindObjectOfType<Revive>().ReviveCurrentLocation();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointTravel : MonoBehaviour
{
    public GameObject[] Buttons;
    public bool isActive;

    private void Start()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(false);
        }
    }

    public void SpawnItems()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(true);
        }
    }

    public void RemoveItems()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(false);
        }
    }


}

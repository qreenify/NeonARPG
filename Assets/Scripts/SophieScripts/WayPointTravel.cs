using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointTravel : MonoBehaviour
{
    public GameObject[] Button;

    private void Start()
    {
        for (int i = 0; i < Button.Length; i++)
        {

            Button[i].SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            for (int i = 0; i < Button.Length; i++)
            {

                Button[i].SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            for (int i = 0; i < Button.Length; i++)
            {

                Button[i].SetActive(false);
            }
        }
    }
}

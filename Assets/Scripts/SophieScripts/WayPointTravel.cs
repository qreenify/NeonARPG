using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointTravel : MonoBehaviour
{
    public GameObject Button;

    private void Start()
    {
        Button.SetActive(false);
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Button.SetActive(true);
            //foreach (WayPointTravel wp in FindObjectsOfType<WayPointTravel>()) {

            // if (wp.code == code && wp != this)
            // {
            //     wp.time = 2;
            //     Vector3 position = wp.gameObject.transform.position;                   
            //     position.y += 2;
            //     collision.gameObject.transform.position = position;
            // }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Button.SetActive(false);
        }
    }
}

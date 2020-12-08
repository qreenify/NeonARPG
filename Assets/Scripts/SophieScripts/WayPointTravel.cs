using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointTravel : MonoBehaviour
{
    public int code;
    float time = 0;

    private void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player" && time <= 0)
        {
            foreach (WayPointTravel wp in FindObjectsOfType<WayPointTravel>()) {

                if (wp.code == code && wp != this)
                {
                    wp.time = 2;
                    Vector3 position = wp.gameObject.transform.position;                   
                    position.y += 2;
                    collision.gameObject.transform.position = position;
                }
            }

        }
    }
}

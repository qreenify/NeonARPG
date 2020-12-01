using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed;
    public float destroyAfter = 10f;
    private void Start()
    {
        Destroy(gameObject, destroyAfter);
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, speed * Time.deltaTime))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            //TODO Apply damage
            Destroy(gameObject);
        }
        else
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
            Debug.DrawRay(transform.position, transform.forward * (speed * Time.deltaTime), Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
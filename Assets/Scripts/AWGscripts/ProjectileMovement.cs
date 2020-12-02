using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed;
    public float destroyAfter = 10f;
    private float _damage;
    public void Setup(Vector3 targetPosition, float damage)
    {
        transform.LookAt(targetPosition);
        this._damage = damage;
        Destroy(gameObject, destroyAfter);
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, speed * Time.deltaTime))
        {
            if (hit.collider.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            //TODO Apply _damage
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
using System;
using UnityEngine;
using unit = Unit.Unit;

[RequireComponent(typeof(unit))] [RequireComponent(typeof(SphereCollider))]
public class Sensor : MonoBehaviour
{
    public float range;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             GetComponent<unit>().target = other.transform;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<unit>().target = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnValidate()
    {
        GetComponent<SphereCollider>().radius = range;
    }
}

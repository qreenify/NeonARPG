using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShowNavMeshVelocity : MonoBehaviour
{
    public float speed;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        speed = navMeshAgent.velocity.magnitude;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour {
    [SerializeField] Transform destination;
    NavMeshAgent navMeshAgent;
    void Start() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update() 
    {
        navMeshAgent.destination = destination.position;
    }
}

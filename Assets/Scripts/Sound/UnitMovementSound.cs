using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovementSound : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public SoundPlayer hoveringSound;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        hoveringSound.ParameterValue = Mathf.Clamp(navMeshAgent.velocity.magnitude, 0, 1);
    }
}

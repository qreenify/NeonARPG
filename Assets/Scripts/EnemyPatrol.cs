using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public float range;
    public List<Vector3> destinations;
    private NavMeshAgent _agent;
    private int _destinationIndex;

    private int DestinationIndex
    {
        get => _destinationIndex;
        set => _destinationIndex = value == destinations.Count ? 0 : value;
    }

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = destinations[DestinationIndex];
    }
    
    void Update()
    {
        if (!InRange()) return;
        DestinationIndex++;
        _agent.destination = destinations[DestinationIndex];
    }

    bool InRange()
    {
        var difference= transform.position - destinations[DestinationIndex];
        return Mathf.Abs(difference.x) < range && Mathf.Abs(difference.y) < range && Mathf.Abs(difference.z) < range;
    }

    [ContextMenu("SavePos")]
    void SavePos()
    {
        destinations.Add(transform.position);
    }
}

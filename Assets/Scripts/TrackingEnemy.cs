using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyPatrol))]
public class TrackingEnemy : MonoBehaviour
{
    public float trackingRange;
    public GameObject player;
    private NavMeshAgent _agent;
    public float chaseSpeed;
    public float coolDown;
    private float _coolDown;
    private bool _coolDownFinshed;
    private EnemyPatrol _enemyPatrol;

    private bool InRange => Vector3.Distance(player.transform.position, transform.position) < trackingRange;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyPatrol = GetComponent<EnemyPatrol>();
    }

    private void Update()
    {
        if (InRange)
        {
            Chase();
            _coolDown = coolDown;
        }
        else
        {
            if (_coolDown > 0)
            {
                _coolDown -= Time.deltaTime;
                _coolDownFinshed = false;
                Chase();
            }
            if (_coolDown <= 0 && !_coolDownFinshed)
            {
                _coolDownFinshed = true;
                _enemyPatrol.SetDestination();
            }
        }
    }

    void Chase()
    {
        _agent.destination = player.transform.position;
        _agent.speed = chaseSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, trackingRange);
    }
}

using UnityEngine;
using UnityEngine.AI;

public class TrackingEnemy : MonoBehaviour
{
    public float trackingRange;
    public GameObject player;
    private NavMeshAgent _agent;
    public float chaseSpeed;
    public float coolDown;
    private float _coolDown;
    private bool _coolDownFinished;

    private bool InRange => Vector3.Distance(player.transform.position, transform.position) < trackingRange;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
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
                _coolDownFinished = false;
                Chase();
            }
            if (_coolDown <= 0 && !_coolDownFinished)
            {
                _coolDownFinished = true;
                if (TryGetComponent(out EnemyPatrol enemyPatrol))
                {
                    enemyPatrol.SetDestination();
                }
                else
                    _agent.destination = transform.position;
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

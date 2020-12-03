using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Unit
{
    [RequireComponent(typeof(Unit))]
    public class EnemyPatrol : UnitAction
    {
        public float range = 1.5f;
        public List<Vector3> destinations;
        private NavMeshAgent _agent;
        private int _destinationIndex;
        public float patrolSpeed = 3;
        public float coolDown = 1;
        private float _coolDown;
        private float _originalSpeed;

        private int DestinationIndex
        {
            get => _destinationIndex;
            set => _destinationIndex = value == destinations.Count ? 0 : value;
        }

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _coolDown = coolDown;
        }

        public override bool IsPossible()
        {
            return unit.target == null;
        }

        public override bool Enter()
        {
            _originalSpeed = _agent.speed;
            SetDestination();
            return true;
        }

        public override bool DoUpdate()
        {
            if (!InRange()) return IsPossible();
            if (_coolDown > 0)
            {
                _coolDown -= Time.deltaTime;
            }
            else
            {
                DestinationIndex++;
                SetDestination();
                _coolDown = coolDown;
            }

            return IsPossible();
        }

        public override bool Exit()
        {
            _agent.speed = _originalSpeed;
            return true;
        }

        public void SetDestination()
        {
            _agent.destination = destinations[DestinationIndex];
            _agent.speed = patrolSpeed;
        }

        bool InRange()
        {
            var difference = transform.position - destinations[DestinationIndex];
            return Mathf.Abs(difference.x) < range && Mathf.Abs(difference.y) < range &&
                   Mathf.Abs(difference.z) < range;
        }

        [ContextMenu("SavePos")]
        void SavePos()
        {
            destinations.Add(transform.position);
        }

        void OnDrawGizmos()
        {
            if (destinations.Count > 1)
            {
                for (int i = 0; i < destinations.Count; i++)
                {
                    Gizmos.color = Color.red;
                    if (i == destinations.Count - 1)
                    {
                        Gizmos.DrawLine(destinations[i], destinations[0]);
                        Gizmos.DrawSphere(destinations[0], 1);
                    }
                    else
                    {
                        Gizmos.DrawLine(destinations[i], destinations[i + 1]);
                        Gizmos.DrawSphere(destinations[i + 1], 1);
                    }
                }
            }
        }
    }
}

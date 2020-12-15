using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Unit
{
    [RequireComponent(typeof(Unit))]
    public class Patrol : UnitAction
    {
        public float range = 1.5f;
        public List<Vector3> destinations = new List<Vector3>();
        private NavMeshAgent _agent;
        private int _destinationIndex;
        public float patrolSpeed = 3;
        public float coolDown = 1;
        private float _coolDown;
        private float _originalSpeed;

        private int DestinationIndex
        {
            get => _destinationIndex;
            set 
            {
                if (value >= destinations.Count)
                    value = 0;
                _destinationIndex = value ;
            }
        }

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _coolDown = coolDown;
            DestinationIndex = GetClosestPoint();
        }

        int GetClosestPoint()
        {
            int closestPoint = -1;
            float closestRange = 0;
            for (int i = 0; i < destinations.Count; i++)
            {
                if(closestPoint == -1 || Vector3.Distance(transform.position, destinations[i]) < closestRange)
                {
                    closestPoint = i;
                    closestRange = Vector3.Distance(transform.position, destinations[i]);
                }
            }
            return closestPoint;
        }

        public override bool IsPossible()
        {
            if(destinations.Count == 0)
            {
                return false;
            }
            return true;
        }

        public override bool Enter()
        {
            _originalSpeed = _agent.speed;
            SetDestination();
            return true;
        }

        private void Update()
        {
            if (IsPossible() && !InRange() && _coolDown > 0)
            {
                _coolDown -= Time.deltaTime;
            }
        }

        public override bool DoUpdate()
        {
            if (!InRange()) return IsPossible();
            if (_coolDown < 0)
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
            Debug.Log(_destinationIndex);
            return Vector3.Distance(transform.position, destinations[DestinationIndex]) < range;
        }

        [ContextMenu("ClearPos")]
        void ClearPos()
        {
            destinations.Clear();
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
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(destinations[0], transform.position);
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

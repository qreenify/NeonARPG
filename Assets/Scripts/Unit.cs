using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Unit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Unit : MonoBehaviour
    {
        public List<UnitAction> possibleActions = new List<UnitAction>();
        public UnitAction currentAction;
        public string tagToSearchFor = "Player";

        private NavMeshAgent navMeshAgent;

        public Transform target;

        public float interactionRange;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            foreach(UnitAction action in GetComponents<UnitAction>())
            {
                possibleActions.Add(action);
                action.unit = this;
            }
            SortActions();
        }
        void Update()
        {
            if (TryGetComponent(out Mover mover))
            {
                currentAction.DoUpdate();
                return;
            }
            SetCurrentAction();
            if(currentAction != null && !currentAction.DoUpdate())
            {
                currentAction.Exit();
                currentAction = null;
            }
        }

        //Unit Actions Selection
        void SetCurrentAction()
        {
            foreach (UnitAction action in possibleActions)
            {
                if (action.IsPossible())
                {
                    SetAction(action);
                    break;
                }
            }
        }
        void SetAction(UnitAction action)
        {
            if (action == currentAction) return;
            if (currentAction != null)
            {
                currentAction.Exit();
            }
            currentAction = action;
            currentAction.Enter();
        }

        //Shared Action Methods
        public bool InRange(Vector3 position)
        {
            return Vector3.Distance(transform.position, position) < interactionRange;
        }
        public bool TargetInView()
        {
            return Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized,
                out RaycastHit ray) && ray.collider.gameObject.CompareTag(tagToSearchFor);
        }
        public void MoveTo(Vector3 position)
        {
            if (position != navMeshAgent.destination)
            {
                Debug.Log("Moving!");
                navMeshAgent.destination = position;
            }
        }
        public void StopMove()
        {
            navMeshAgent.destination = transform.position;
        }

        public void SortActions()
        {
            var comparer = new UnitActionComparer();
            possibleActions.Sort(comparer);
        }
        //What's needed:
        //Melee attack
        //Ranged attack
        //In range
    }

    public abstract class UnitAction : MonoBehaviour
    {
        public Unit unit;
        public int priority;
        public abstract bool IsPossible();
        public abstract bool Enter();
        public abstract bool DoUpdate();
        public abstract bool Exit();
    }

    public class UnitActionComparer : IComparer<UnitAction>
    {
        public int Compare(UnitAction x, UnitAction y)
        {
            return y.priority.CompareTo(x.priority);
        }
    }
}

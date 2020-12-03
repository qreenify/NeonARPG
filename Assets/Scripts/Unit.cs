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
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                //Queue move + Melee attack...
            }

            if (currentAction == null)
            {
                SetCurrentAction();
            }
            else if(!currentAction.DoUpdate())
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
            return true;
        }
        public void MoveTo(Vector3 position)
        {
            if(position != navMeshAgent.destination)
                navMeshAgent.SetDestination(position);
        }
        public void StopMove()
        {
            navMeshAgent.isStopped = true;
        }
        //What's needed:
        //Melee attack
        //Ranged attack
        //In range
    }

    public abstract class UnitAction : MonoBehaviour
    {
        public Unit unit;
        public abstract bool IsPossible();
        public abstract bool Enter();
        public abstract bool DoUpdate();
        public abstract bool Exit();
    }
}

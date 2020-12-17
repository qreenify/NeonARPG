using UnityEngine;

namespace Unit
{
    public class Approach : UnitAction
    {
        public float maxRange = 10;
        public bool showGizmos = true;
    
        public bool InMaxRange
        {
            get => Vector3.Distance(transform.position, unit.target.position) < maxRange;
        }
        public bool PlayerInView
        {
            get
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, PlayerController.playerController.transform.position, out hit))
                {
                    if (hit.collider.gameObject == PlayerController.playerController.gameObject)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public override bool IsPossible()
        {
            if (unit.target == null || !unit.target.gameObject.activeSelf || !InMaxRange || !unit.TargetInView())
            {
                return false;
            }
            return true;
        }
        public override bool Enter()
        {
            return true;
        }
        public override bool DoUpdate()
        {
            if (!IsPossible())
            {
                return false;
            }
            DoApproach();
            return IsPossible();
        }
        public override bool Exit()
        {
            return true;
        }

        public void DoApproach()
        {
            if (IsPossible())
            {
                Vector3 direction = unit.target.position;
                //transform.position += unit.target.position - transform.position;

                unit.MoveTo(direction);
            }
        }

        private void OnDrawGizmos()
        {
            if (showGizmos)
            {
                Gizmos.color = Color.red;
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, maxRange);
            }
        }
    }
}
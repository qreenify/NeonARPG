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

        public override bool IsPossible()
        {
            if (unit.target == null || !unit.target.gameObject.activeSelf || !InMaxRange)
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
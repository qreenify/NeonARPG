using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class UnitFlee : UnitAction
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
            Flee();
            return IsPossible();
        }
        public override bool Exit()
        {
            return true;
        }

        public void Flee()
        {
            if (IsPossible())
            {
                Vector3 targetPosition = unit.target.position;
                //transform.position += unit.target.position - transform.position;

                unit.MoveTo(((transform.position - targetPosition).normalized * 10) + transform.position);
            }
        }

        private void OnDrawGizmos()
        {
            if (showGizmos)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, maxRange);
            }
        }
    }
}
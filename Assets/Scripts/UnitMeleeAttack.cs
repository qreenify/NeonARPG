using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(Unit))]
    public class UnitMeleeAttack : UnitAction
    {
        public float maxRange = 10;
        public float range = 2;
        public float attackDamage = 10;

        public bool InAttackRange
        {
            get => Vector3.Distance(transform.position, unit.target.position) < range;
        }
        public bool InMaxRange
        {
            get => Vector3.Distance(transform.position, unit.target.position) < maxRange;
        }

        public override bool IsPossible()
        {
            if(unit.target == null || !unit.target.gameObject.activeSelf || !InMaxRange)
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
            if (Attack())
            {
                return false;
            }
            return IsPossible();
        }
        public override bool Exit()
        {
            return true;
        }
        bool Attack()
        {
            if (InAttackRange)
            {
                unit.StopMove();
                Debug.Log("Damage!");
                unit.target.GetComponent<Health>().TakeDamage(attackDamage);
                return true;
            }
            else
            {
                unit.MoveTo(unit.target.position);
                return false;
            }
        }
    }
}
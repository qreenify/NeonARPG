using System;
using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(Unit))]
    public class UnitMeleeAttack : UnitAction
    {
        public float maxRange = 10;
        public float range = 2;
        public float attackDamage = 10;
        public float coolDown = 3;
        private float _currentCooldown;
        
        public bool CooldownFinished => _currentCooldown <= 0;
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

        private void Update()
        {
            if (_currentCooldown > 0) _currentCooldown -= Time.deltaTime;
        }

        bool Attack()
        {
            if (InAttackRange && CooldownFinished)
            {
                unit.StopMove();
                Debug.Log("Damage!");
                unit.target.GetComponent<Health>().TakeDamage(attackDamage);
                _currentCooldown = coolDown;
                return true;
            }
            else
            {
                unit.MoveTo(unit.target.position);
                return false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
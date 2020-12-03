using System;
using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(Unit))]
    public class UnitRangedAttack : UnitAction
    {
        [Tooltip("Do Not tuchies")]
        public float minRange;
        public float maxRange = 10;
        public float range = 5;
        public float attackDamage = 10;
        public float coolDown = 3;
        private float _currentCooldown;
        public bool showGizmos = true;

        public bool CooldownFinished => _currentCooldown <= 0;
        public bool InAttackRange
        {
            get => Vector3.Distance(transform.position, unit.target.position) < range;
        }
        public bool InRange
        {
            get => Vector3.Distance(transform.position, unit.target.position) < maxRange && Vector3.Distance(transform.position, unit.target.position) > minRange;
        }

        public override bool IsPossible()
        {
            if(unit.target == null || !unit.target.gameObject.activeSelf || !InRange)
            {
                return false;
            }
            return true;
        }
        public override bool Enter()
        {
            unit.MoveTo(unit.target.position);
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

        public void OnValidate()
        {
            if(minRange > maxRange)
            {
                maxRange = minRange;
            }
        }

        bool Attack()
        {
            if (InAttackRange && unit.TargetInView())
            {
                unit.StopMove();
                if (CooldownFinished)
                {
                    Debug.Log("Damage!");
                    unit.target.GetComponent<Health>().TakeDamage(attackDamage);
                    _currentCooldown = coolDown;
                    return true;
                }
                transform.LookAt(new Vector3(unit.target.position.x, transform.position.y, unit.target.position.z));
                return false;
            }
            else
            {
                //transform.position = unit.target.position;
                unit.MoveTo(unit.target.position);
                return false;
            }
        }

        private void OnDrawGizmos()
        {
            if (showGizmos)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, range);
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, maxRange);
                Gizmos.color = Color.gray;
                Gizmos.DrawWireSphere(transform.position, minRange);
            }
        }
    }
}
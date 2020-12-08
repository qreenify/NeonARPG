using UnityEngine;
using UnityEngine.AI;

namespace Unit
{
    [RequireComponent(typeof(Unit))]
    public class RangedAttack : UnitAction
    {
        [Tooltip("The Range At Which The AI Will Stop And Do Damage")]
        public float range = 5;
        public float attackDamage = 10;
        public float coolDown = 3;
        public float windUpTime = 0.4f;
        private float _currentCooldown;
        private float _windUpTime;
        private NavMeshAgent _agent;
        public bool showGizmos = true;

        public bool CooldownFinished => _currentCooldown <= 0;
        public bool WindUpFinished => _windUpTime <= 0;
        public bool InAttackRange
        {
            get => Vector3.Distance(transform.position, unit.target.position) < range;
        }

        private void Start()
        {
            _windUpTime = windUpTime;
            _agent = GetComponent<NavMeshAgent>();
        }

        public override bool IsPossible()
        {
            if(unit.target == null || !unit.target.gameObject.activeSelf || !InAttackRange)
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
            
            if (_windUpTime > 0 && unit.target != null && _agent.velocity.magnitude == 0) 
                _windUpTime -= Time.deltaTime;
            else if (unit.target == null || _agent.velocity.magnitude != 0) 
                _windUpTime = windUpTime;
        }

        bool Attack()
        {
            if (!IsPossible())
            {
                return false;
            }
            if (InAttackRange && unit.TargetInView())
            {
                unit.StopMove();
                if (CooldownFinished && WindUpFinished)
                {
                    //Debug.Log("Damage!");
                    unit.target.GetComponent<Health>().TakeDamage(attackDamage);
                    _currentCooldown = coolDown;
                    _windUpTime = windUpTime;
                    return true;
                }
                transform.LookAt(new Vector3(unit.target.position.x, transform.position.y, unit.target.position.z));
                return false;
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            if (showGizmos)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, range);
            }
        }
    }
}
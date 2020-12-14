using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

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
        private float _startAttackDamage;
        public bool showGizmos = true;
        public event Action ONCancelAttack, ONLoadingAttack, ONAttack;

        public bool CooldownFinished => _currentCooldown <= 0;
        public bool WindUpFinished => _windUpTime <= 0;
        public bool InAttackRange
        {
            get => Vector3.Distance(transform.position, unit.target.position) < range;
        }
        
        public bool IsLoadingAttack { get; set; }

        private void Start()
        {
            _windUpTime = windUpTime;
            _agent = GetComponent<NavMeshAgent>();
            _startAttackDamage = attackDamage;

            if (TryGetComponent<PlayerLevel>(out var level))
            {
                SetDamage(level);
                level.ONLevelUp += SetDamage;
            }
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
            if (TryGetComponent<PlayerController>(out var playerController))
            {
                unit.MoveTo(unit.target.position);
            }
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

            if (_windUpTime > 0 && unit.target != null && _agent.velocity.magnitude == 0 && CooldownFinished) 
                _windUpTime -= Time.deltaTime;
            else if (unit.target == null || _agent.velocity.magnitude != 0)
            {
                ONCancelAttack?.Invoke();
                _windUpTime = windUpTime;
            }
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
                if (!IsLoadingAttack && CooldownFinished)
                {
                    ONLoadingAttack?.Invoke();
                }

                if (CooldownFinished && WindUpFinished)
                {
                    //Debug.Log("Damage!");
                    ONCancelAttack?.Invoke();
                    ONAttack?.Invoke();
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

        private void SetDamage(PlayerLevel level)
        {
            attackDamage = _startAttackDamage + level.rangedDamageIncrease * level.level;
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
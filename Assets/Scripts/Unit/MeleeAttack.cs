using System;
using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(Unit))]
    public class MeleeAttack : UnitAction
    {
        public GameObject meeleeDisplay;

        public DrawAttackLine drawAttackLine;
        public float range = 2;
        public float attackDamage = 10;
        public float coolDown = 3;
        private float _currentCooldown;
        public bool showGizmos = true;
        private float _startAttackDamage;

        [FMODUnity.EventRef]
        public string meleeSound = "event:/SFX/Enemies/enemySword_SFX";
        public bool CooldownFinished => _currentCooldown <= 0;
        public bool InAttackRange
        {
            get => Vector3.Distance(transform.position, unit.target.position) < range;
        }

        private void Start()
        {
            _startAttackDamage = attackDamage;
            if (TryGetComponent<PlayerLevel>(out var level))
            {
                SetDamage(level);
                level.ONLevelUp += SetDamage;
            }
        }

        private void SetDamage(PlayerLevel level)
        {
            attackDamage = _startAttackDamage + level.meleeDamageIncrease * level.level;
        }

        public override bool IsPossible()
        {
            if (unit.target == null || !unit.target.gameObject.activeSelf || !InAttackRange)
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
            if (!IsPossible() ||  Attack())
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
            if (InAttackRange && unit.TargetInView())
            {          
                unit.StopMove();
                transform.LookAt(new Vector3(unit.target.position.x, transform.position.y, unit.target.position.z));
                if (CooldownFinished)
                {
                    if (meleeSound != null && meleeSound != "")
                    {
                        GlobalSoundPlayer.globalSoundPlayer.PlaySound(meleeSound);
                    }
                    /////////////////////////////////////////////////Meelee Feedback Instantiation//////////////////////////////////
                    //if (meeleeDisplay != null)
                    //{
                    //   var thisMeleeDisplay = Instantiate(meeleeDisplay, unit.target.transform.position, unit.target.transform.rotation);
                    //    thisMeleeDisplay.transform.SetParent(unit.target.transform);
                    //    Debug.Log(thisMeleeDisplay.transform.parent);
                    //    //meeleeParticleSystem.StartParticleSystem();
                    //}
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //Debug.Log("Damage!");
                    unit.target.GetComponent<Health>().CurrentHealth -= attackDamage;
                    _currentCooldown = coolDown;
                    drawAttackLine?.DrawLine(unit.target);
                    return true;
                }
                return false;
            }
           // meeleeParticleSystem.StopParticleSystem();
            //Destroy(meeleeParticleSystem);
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
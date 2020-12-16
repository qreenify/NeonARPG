using UnityEngine;

namespace Unit
{
    public class RangedAttackVFX : MonoBehaviour
    {
        [SerializeField] private float beamLifeTime = 0.5f;
        [SerializeField] GameObject beamVFXPrefab;
        [SerializeField] GameObject loadFieldVFXPrefab;
        private Unit _unit;
        private bool _isLoadingAttacking;
        private float _beamInstantiateTime;
        private RangedAttack _rangedAttack;
        private GameObject _beamInstance, _loadFieldInstance;
        
        private void Start()
        {
            _rangedAttack = GetComponentInParent<RangedAttack>();
            _rangedAttack.ONCancelAttack += CancelAttack;
            _rangedAttack.ONAttack += Attack;
            _rangedAttack.ONLoadingAttack += LoadAttack;
            if (TryGetComponent<PlayerController>(out var playerController))
            {
                playerController.ONWeaponSwap += WeaponSwapped;
            }
            _unit = GetComponentInParent<Unit>();
        }

        private void Update()
        {
            if (_beamInstantiateTime > 0) _beamInstantiateTime -= Time.deltaTime;
            if (_beamInstantiateTime < 0) DestroyBeam();
        }

        void WeaponSwapped(bool ranged)
        {
            if (ranged == false)
            {
                CancelAttack();
            }
        }

        void CancelAttack()
        {
            _rangedAttack.IsLoadingAttack = false;
            if (_loadFieldInstance != null)
            {
                Destroy(_loadFieldInstance);
            }
        }
        
        void LoadAttack()
        {
            _loadFieldInstance = Instantiate(loadFieldVFXPrefab, transform);
            _rangedAttack.IsLoadingAttack = true;
        }

        void Attack()
        {
            _beamInstance = Instantiate(beamVFXPrefab, transform);
            _beamInstantiateTime = beamLifeTime;
            _beamInstance.transform.LookAt(_unit.target.position);
            _beamInstance.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, Vector3.Distance(transform.position, _unit.target.position) / transform.parent.localScale.x));
            _beamInstance.transform.parent = null;
        }

        void DestroyBeam()
        {
            if (_beamInstance != null)
            {
                Destroy(_beamInstance);
            }
        }
    }
}


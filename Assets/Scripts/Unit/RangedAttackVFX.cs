using System;
using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(RangedAttack))]
    public class RangedAttackVFX : MonoBehaviour
    {
        [SerializeField] GameObject beamVFXPrefab;
        [SerializeField] GameObject loadFieldVFXPrefab;
        [SerializeField] Transform beamSpawnPoint;
        private Unit _unit;
        private bool _isLoadingAttacking;
        private float _beamInstantiateTime;
        private RangedAttack _rangedAttack;
        private GameObject _beamInstance, _loadFieldInstance;
        
        private void Start()
        {
            _rangedAttack = GetComponent<RangedAttack>();
            _rangedAttack.ONCancelAttack += CancelAttack;
            _rangedAttack.ONAttack += Attack;
            _rangedAttack.ONLoadingAttack += LoadAttack;
            _unit = GetComponent<Unit>();
        }

        private void Update()
        {
            if (_beamInstantiateTime > 0) _beamInstantiateTime -= Time.deltaTime;
            if (_beamInstantiateTime < 0) DestroyBeam();
        }

        void CancelAttack()
        {
            _rangedAttack.IsLoadingAttack = false;
            _beamInstantiateTime = 0.5f;
            Destroy(_loadFieldInstance);
        }
        
        void LoadAttack()
        {
            _loadFieldInstance = Instantiate(loadFieldVFXPrefab, beamSpawnPoint.transform);
            _rangedAttack.IsLoadingAttack = true;
        }

        void Attack()
        {
            _beamInstance = Instantiate(beamVFXPrefab, beamSpawnPoint.transform);
            _beamInstance.transform.LookAt(_unit.target.position);
            _beamInstance.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, Vector3.Distance(transform.position, _unit.target.position) / 3));
        }

        void DestroyBeam()
        {
            Destroy(_beamInstance);
        }
    }
}


using System;
using UnityEngine;

    public class DamageArea : MonoBehaviour
    {
        public float dps = 1.0f;
        public float damageIntervalInSec = 0.5f;
        private float _timeCounter;

        void OnTriggerStay(Collider other) 
        {
            if (!other.isTrigger && other.TryGetComponent(out Health health))
            {
                _timeCounter += Time.deltaTime;
                if (_timeCounter >= damageIntervalInSec)
                {
                    health.CurrentHealth -= dps * damageIntervalInSec;
                    _timeCounter -= damageIntervalInSec;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Health health))
            {
                _timeCounter = damageIntervalInSec;
            }
        }
    }


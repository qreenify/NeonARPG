using System;
using UnityEngine;

    public class DamageArea : MonoBehaviour
    {
        public float dps = 1.0f;

        void OnTriggerStay(Collider other) {
            if (other.TryGetComponent(out Health health)) 
            {
                health.TakeDamage(dps * Time.fixedDeltaTime);
            }
        }
    }


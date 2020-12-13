using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(SphereCollider))]
    public class Sensor : MonoBehaviour
    {
        public float range;

        private void Awake()
        {
            GetComponent<SphereCollider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && other.GetComponent<Unit>().isActiveAndEnabled)
            {
                GetComponentInParent<Unit>().target = other.transform;
            }
        }
    
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GetComponentInParent<Unit>().target = null;
            }
        }

        public void OnPlayerDeath()
        {
            GetComponentInParent<Unit>().target = null;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, range);
        }

        private void OnValidate()
        {
            GetComponent<SphereCollider>().radius = range;
        }
    }
}

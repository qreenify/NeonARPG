using System;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public float damage;
    public float range;
    public float timeBetweenAttacks;
    public Transform debugSphere;
    private float _currentCooldown;

    public bool ReadyToAttack => _currentCooldown < 0;

    private void Update()
    {
        _currentCooldown -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        var newPos = debugSphere.position;
        newPos.y = 0.1f;
        debugSphere.position = newPos;
    }

    public void Attack(GameObject target)
    {
        if (InRange(target.transform) && ReadyToAttack)
        {
            target.GetComponent<Health>().TakeDamage(damage);
            _currentCooldown = timeBetweenAttacks;
        }
    }
    private bool InRange(Transform target) => Vector3.Distance(target.position, transform.position) < range;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }
}

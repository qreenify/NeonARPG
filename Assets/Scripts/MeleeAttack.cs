using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float damage;
    public float range;
    public float timeBetweenAttacks;

    private void Attack(Transform target)
    {
    }
    
    private bool InRange(Transform target) => Vector3.Distance(target.position, transform.position) < range;
}

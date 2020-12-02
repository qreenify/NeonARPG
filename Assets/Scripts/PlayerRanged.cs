using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerRanged : MonoBehaviour
{
    public float timeToReload = 5f;
    public float reloadTime;
    public float damage;
    public float attackDelay;
    public ProjectileMovement projectilePrefab;
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (reloadTime < timeToReload)
            reloadTime += Time.deltaTime;
    }
    
    public void TrySpawnProjectile(Vector3 position)
    {
        if (reloadTime >= timeToReload && _agent.velocity == Vector3.zero)
        {
            StartCoroutine(SpawnProjectile(position));
        }
    }

    IEnumerator SpawnProjectile(Vector3 position)
    {
        yield return new WaitForSeconds(attackDelay);
        if (_agent.velocity != Vector3.zero) yield break;
        transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
        reloadTime = 0;
        var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.Setup(position, damage);
    }
}

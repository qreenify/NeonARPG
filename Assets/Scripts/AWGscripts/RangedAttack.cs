using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public float timeToReload = 5f;
    public float reloadTime;
    public float damage;
    public Transform target;
    public ProjectileMovement projectilePrefab;

    private void Update()
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        reloadTime += Time.deltaTime;
        if (reloadTime >= timeToReload)
        {
            SpawnProjectile();
            reloadTime -= timeToReload;
        }
    }

    private void SpawnProjectile()
    {
        ProjectileMovement projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.Setup(target.position, damage);
    }
}

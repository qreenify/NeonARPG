using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : AIBehaviour
{
    public float timeToReload = 5f;
    public float reloadTime;
    public float damage;
    public Transform target;
    public ProjectileMovement projectilePrefab;

    void Start()
    {
        
    }

    public override bool DoUpdate()
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        reloadTime += Time.deltaTime;
        if (reloadTime >= timeToReload)
        {
            SpawnProjectile();
            reloadTime -= timeToReload;
        }
        return true;
    }

    private void SpawnProjectile()
    {
        ProjectileMovement projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.Setup(target.position, damage);
    }
}

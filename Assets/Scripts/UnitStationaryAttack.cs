using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStationaryAttack : AIBehaviour
{
    public float timeToReload = 2f;
    public float damage;
    public Transform target;
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    public float attackRange = 5;
    
    [SerializeField] Transform objectToPan;

    void FixedUpdate()
    {
        LookAtEnemy();
    }

    public override bool DoUpdate()
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        timeToReload += Time.deltaTime;
        if (timeToReload >= 0)
        {
            //timeToReload -= timeToReload;
        }
        return true;
    }

    void LookAtEnemy()
    {
        objectToPan.LookAt(target);
    }
}

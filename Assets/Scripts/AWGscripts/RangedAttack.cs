using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public float timeToReload = 5f;
    public float reloadTime;
    public GameObject target;
    public GameObject projectilePrefab;

    private void Update()
    {
        transform.LookAt(target.transform);
        reloadTime += Time.deltaTime;
        if (reloadTime >= timeToReload)
        {
            SpawnProjectile();
            reloadTime -= timeToReload;
        }
    }

    private void SpawnProjectile()
    {
        Instantiate(projectilePrefab, transform.position, transform.rotation);
    }
}

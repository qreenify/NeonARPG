using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHealing : MonoBehaviour
{
    public float addHealth = 5;
    private Health health;
    public float time;
    public float healTime;

    private void Start()
    {
        health = GetComponent<Health>();
    }
    void Update()
    {
      
        AutoHeal();
    }

    public void AutoHeal()
    {
        if (health.CurrentHealth < health.maxHealth)
        {
            health.CurrentHealth += addHealth * Time.deltaTime;
        }
    }
}

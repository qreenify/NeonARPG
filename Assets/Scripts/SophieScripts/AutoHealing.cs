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
        if(health.CurrentHealth < health.maxHealth)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0.0f;
        }
        AutoHeal();
    }

    public void AutoHeal()
    {
        if(time >=healTime)
        {
            health.CurrentHealth += addHealth;
            time -= healTime;
        }
    }
}

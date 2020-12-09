using System;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int amount;
    [Range(0, 1.0f)]
    public float healPercentage = 0.2f;
    public KeyCode drinkKey = KeyCode.Space;

    private int Amount
    {
        get => amount;
        set
        {
            amount = value;
            Debug.Log("Health Potions " + amount);
        }
    }

    public void Add()
    {
        Amount++;
    }

    private void Update()
    {
        if (Input.GetKeyDown(drinkKey))
        {
            Use();
        }
    }

    public void Use()
    {
        if (amount > 0)
        {
            Amount--;
            var health = GetComponent<Health>();
            var addHealth = health.maxHealth / healPercentage;
            health.CurrentHealth += addHealth;
        }
    }
}

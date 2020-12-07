﻿using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public AutoHealing autoHealing;
    [SerializeField] private float currentHealth;
    public UnityEvent<float> onMaxHealthSet;
    public UnityEvent<string> onHealthUI;
    public UnityEvent<float> onHealthChanged;
    public UnityEvent<float> onDamageTaken;
    public UnityEvent<float> onHealthIncreased;
    public UnityEvent onDefeat;
    public UnityEvent onRevive;

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (value < currentHealth)
                onDamageTaken.Invoke(currentHealth - value);
            else if (value > currentHealth) 
                onHealthIncreased.Invoke(value - currentHealth);
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            onHealthUI.Invoke(currentHealth.ToString());
            Debug.Log($"{gameObject.name} {currentHealth}/{maxHealth}");
            Defeat();
            onHealthChanged.Invoke(currentHealth);
        }
    }

    private void Start()
    {
        onMaxHealthSet.Invoke(maxHealth);
    }
    
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        onDamageTaken.Invoke(damage);
    }

    [ContextMenu("TakeDamage")]
    public void TakeDamage()
    {
        CurrentHealth -= 5;
        onDamageTaken.Invoke(5);
    }
    
    public void Defeat()
    {
        if (CurrentHealth > 0)
        {
            return;
        }
        onDefeat.Invoke();
        gameObject.SetActive(false);
        //TODO: Trigger defeat sound / animation
    }
    
    public void Revive()
    {
        CurrentHealth = maxHealth;
        gameObject.SetActive(true);
        onRevive.Invoke();
        //TODO: Trigger revive sound / animation
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            TakeDamage();
        }
    }

    private void OnValidate()
    {
        currentHealth = maxHealth;
    }
}

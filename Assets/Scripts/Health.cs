﻿using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth;
    private float _currentHealth;
    public UnityEvent<float> onMaxHealthSet;
    public UnityEvent<string> onHealthUI;
    public UnityEvent<float> onHealthChanged;
    public UnityEvent<float> onDamageTaken;
    public UnityEvent onDefeat;
    public UnityEvent onRevive;

    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, maxHealth);
            onHealthUI.Invoke(_currentHealth.ToString());
            Debug.Log($"{gameObject.name} {_currentHealth}/{maxHealth}");
            Defeat();
            onHealthChanged.Invoke(_currentHealth);
        }
    }

    private void Start()
    {
        onMaxHealthSet.Invoke(maxHealth);
        CurrentHealth = maxHealth;
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
        gameObject.SetActive(false);
        onDefeat.Invoke();
        //TODO: Trigger defeat sound / animation
    }
    
    public void Revive()
    {
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
}

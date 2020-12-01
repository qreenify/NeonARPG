using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth;
    private int _health;
    public UnityEvent<string> onHealthChanged;
    public UnityEvent<int> onDamageTaken;

    public int health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, maxHealth);
            onHealthChanged.Invoke(_health.ToString());
            SendMessage("OnHealthChanged", SendMessageOptions.DontRequireReceiver);
            if (_health == 0)
            {
                SendMessage("OnDeath");
            }
        }
    }

    private void Start()
    {
        _health = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        onDamageTaken.Invoke(damage);
        SendMessage("OnDamageTaken", SendMessageOptions.DontRequireReceiver);
    }
    
    [ContextMenu("TakeDamage")]
    public void TakeDamage()
    {
        health -= 5;
        onDamageTaken.Invoke(5);
        SendMessage("OnDamageTaken", SendMessageOptions.DontRequireReceiver);
    }
}

using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth;
    private float _currentHealth;
    public UnityEvent<float> onMaxHealthSet;
    public UnityEvent<string> onHealthUI;
    public UnityEvent<float> onHealthChanged;
    public UnityEvent<float> onDamageTaken;

    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, maxHealth);
            onHealthUI.Invoke(_currentHealth.ToString());
            onHealthChanged.Invoke(_currentHealth);
            SendMessage("OnHealthChanged", SendMessageOptions.DontRequireReceiver);
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
        SendMessage("OnDamageTaken", SendMessageOptions.DontRequireReceiver);
    }
    
    [ContextMenu("TakeDamage")]
    public void TakeDamage()
    {
        CurrentHealth -= 5;
        onDamageTaken.Invoke(5);
        SendMessage("OnDamageTaken", SendMessageOptions.DontRequireReceiver);
    }
}

using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public AutoHealing autoHealing;
    public static bool debug;
    [SerializeField] private float currentHealth;
    public bool useDamagePopUp = true;
    public UnityEvent<float> onMaxHealthSet;
    public UnityEvent<string> onHealthUI;
    public UnityEvent<float> onHealthChanged;
    public UnityEvent<Transform, float> onDamageTaken;
    public UnityEvent<float> onHealthIncreased;
    public UnityEvent onDefeat;
    public UnityEvent onRevive;

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (value < currentHealth)
            {
                onDamageTaken.Invoke(transform, currentHealth - value);
                if (useDamagePopUp)
                    DamagePopUpSpawner.Create(transform, currentHealth - value);
            }
                
            else if (value > currentHealth) 
                onHealthIncreased.Invoke(value - currentHealth);
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            onHealthUI.Invoke(currentHealth.ToString());
            if (debug)
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
        //onDamageTaken.Invoke(this, damage);
    }

    [ContextMenu("TakeDamage")]
    public void TakeDamage()
    {
        CurrentHealth -= 5;
        //onDamageTaken.Invoke(this, 5);
    }
    
    public void Defeat()
    {
        if (CurrentHealth > 0)
        {
            return;
        }
        onDefeat.Invoke();
        var rewards = GetComponents<IReward>();
        foreach (var reward in rewards)
        {
            reward.Reward();
        }

        if (TryGetComponent<Dissolve>(out var dissolve))
        {
            StartCoroutine(dissolve.DoDissolve());
        }
        else
            gameObject.SetActive(false);
        //TODO: Trigger defeat sound / animation
    }
    
    public void Revive()
    {
        CurrentHealth = maxHealth;
        gameObject.SetActive(true);
        onRevive.Invoke();
        if (TryGetComponent(out Unit.Unit unit))
        {
            unit.Clear();
        }
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
        #if UNITY_EDITOR
        if (!UnityEditor.EditorApplication.isPlaying)
            currentHealth = maxHealth;
        #endif
    }

    [ContextMenu("ToggleDebug")]
    private void ToggleDebug()
    {
        debug = !debug;
    }
}

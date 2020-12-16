using System;
using Unit;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public FeedbackDisplays damageFeedback;
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
    private float _baseMaxHealth;

    private void Awake()
    {
        damageFeedback = GetComponent<FeedbackDisplays>();
    }

    [FMODUnity.EventRef]
    public string damageSound;
    [FMODUnity.EventRef]
    public string defeatSound;
    [FMODUnity.EventRef]
    public string reviveSound;


    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (damageSound != null && damageSound != "")
            {
                GlobalSoundPlayer.globalSoundPlayer.PlaySound(damageSound);
            }

            if (value < currentHealth)
            {
                onDamageTaken.Invoke(transform, currentHealth - value);
                if (damageFeedback != null)
                {
                    damageFeedback.DamageFeedback();
                }
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
        if (TryGetComponent<PlayerLevel>(out var level))
        {
            _baseMaxHealth = maxHealth;
            SetMaxHealth(level);
            level.ONLevelUp += SetMaxHealth;
        }
    }
    
    //public void TakeDamage(float damage)
    //{    
    //    CurrentHealth -= damage;
    //    damageFeedback.Feedback();
    //    //onDamageTaken.Invoke(this, damage);
    //}

    //[ContextMenu("TakeDamage")]
    //public void TakeDamage()
    //{
    //    CurrentHealth -= 5;
    //    //onDamageTaken.Invoke(this, 5);
    //}
    
    public void Defeat()
    {
        if (CurrentHealth > 0)
        {
            return;
        }
        if (defeatSound != null && defeatSound != "")
        {
            GlobalSoundPlayer.globalSoundPlayer.PlaySound(defeatSound);
        }
        var rewards = GetComponents<IReward>();
        foreach (var reward in rewards)
        {
            reward.Reward();
        }

        if (TryGetComponent<Dissolve>(out var dissolve))
        {
            StartCoroutine(dissolve.DoDissolve());
            var unit = GetComponent<Unit.Unit>();
            unit.StopMove();
            unit.enabled = false;
        }
        else
        {
            onDefeat.Invoke();
            gameObject.SetActive(false);
        }

        //TODO: Trigger defeat sound / animation
    }
    
    public void Revive()
    {
        CurrentHealth = maxHealth;
        gameObject.SetActive(true);
        onRevive.Invoke();
        if (TryGetComponent<Dissolve>(out var dissolve))
        {
            StartCoroutine(dissolve.DoCondense());
        }
        else if (TryGetComponent(out Unit.Unit unit1))
        {
            unit1.Clear();
        }
        if (reviveSound != null && reviveSound != "")
        {
            GlobalSoundPlayer.globalSoundPlayer.PlaySound(reviveSound);
        }
        //TODO: Trigger revive sound / animation
    }

    private void SetMaxHealth(PlayerLevel level)
    {
        maxHealth = _baseMaxHealth + level.level * level.healthIncrease;
        currentHealth = maxHealth;
        onMaxHealthSet.Invoke(maxHealth);
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

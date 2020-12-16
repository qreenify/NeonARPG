using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthPotion : MonoBehaviour, ISaveable
{
    public int amount;
    [Range(0, 1.0f)]
    public float healPercentage = 0.2f;
    public KeyCode drinkKey = KeyCode.Space;
    public UnityEvent<int> onHealthPotionChange;

    private int Amount
    {
        get => amount;
        set
        {
            onHealthPotionChange.Invoke(value);
            amount = value;
            //Debug.Log("Health Potions " + amount);
        }
    }

    private void Start()
    {
        onHealthPotionChange.Invoke(Amount);
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
        var health = GetComponent<Health>();
        if (health.CurrentHealth == health.maxHealth || health.CurrentHealth == 0) return;
        if (amount > 0)
        {
            Amount--;
            var addHealth = healPercentage / health.maxHealth;
            health.CurrentHealth += addHealth;
        }
    }

    public bool Deserialize(string healthPotions)
    {
        var json = JsonUtility.FromJson<HealthPotionSave>(healthPotions);
        if (json.Equals(new HealthPotionSave())) return false;
        json.ApplyValues(this);
        return true;
    }

    public string Serialize() => JsonUtility.ToJson(new HealthPotionSave(this)); 

    [Serializable]
    internal class HealthPotionSave
    {
        public bool Equals(HealthPotionSave other) => healthPotions == other.healthPotions;
        public int healthPotions;
        public HealthPotionSave(){}
        public HealthPotionSave(HealthPotion healthPotion) => healthPotions = healthPotion.Amount;
        public void ApplyValues(HealthPotion healthPotion) => healthPotion.Amount = healthPotions;
    }
}

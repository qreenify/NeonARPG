using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLevel : MonoBehaviour, ISaveable
{
    public float baseRequiredExp = 100;
    public float increasePerLevel = 5;
    public int level;
    [Header("Increases Per Level")]
    public float rangedDamageIncrease = 1;
    public float meleeDamageIncrease = 1;
    public float healthIncrease = 5;
    public event Action<PlayerLevel> ONLevelUp; 
    [HideInInspector] public UnityEvent<float> onExpChanged;
    [HideInInspector] public UnityEvent<int> onLevelChanged;
    private float _experience;
    FeedbackDisplays feedback;

    public float RequiredExp => increasePerLevel * level + baseRequiredExp;

    public float Experience
    {
        get => _experience;
        private set
        {
            _experience = value;
            if (_experience > RequiredExp)
            {
                IncreaseLevel();
            }
            onExpChanged.Invoke(_experience);
        }
    }
    private void Awake()
    {
        feedback = GetComponent<FeedbackDisplays>();
    }

    public void Increase(float amount)
    {
        Experience += amount;
    }

    public void IncreaseLevel()
    {
        while (true)
        {
            _experience -= RequiredExp;
            level++;
            ONLevelUp?.Invoke(this);
            onLevelChanged.Invoke(level);
            if (feedback != null)
            {
                feedback.LevelUpFeedback();
            }
            if (Experience > RequiredExp)
            {
                continue;
            }

            break;
        }
    }

    public bool Deserialize(string playerLevel)
    {
        var json = JsonUtility.FromJson<LevelSave>(playerLevel);
        if (json.Equals(new LevelSave())) return false;
        json.ApplyValues(this);
        return true;
    }
    
    public string Serialize() => JsonUtility.ToJson(new LevelSave(this));

    [Serializable]
    internal class LevelSave
    {
        public bool Equals(LevelSave other) => experience.Equals(other.experience) && level == other.level;
        public float experience;
        public int level;
        public LevelSave(){}
        public LevelSave(PlayerLevel playerLevel)
        {
            experience = playerLevel.Experience;
            level = playerLevel.level;
        }

        public void ApplyValues(PlayerLevel playerLevel)
        {
            playerLevel._experience = experience;
            playerLevel.level = level;
        }
    }
}

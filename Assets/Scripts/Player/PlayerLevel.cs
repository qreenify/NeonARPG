using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLevel : MonoBehaviour, ISaveable
{
    public float baseRequiredExp = 100;
    public float increasePerLevel = 5;
    public int level;
    [HideInInspector] public UnityEvent<float> onExpChanged;
    [HideInInspector] public UnityEvent<int> onLevelChanged;
    public float _experience;

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

    public void Increase(float amount)
    {
        Experience += amount;
    }

    public void IncreaseLevel()
    {
        onLevelChanged.Invoke(level);
        _experience -= RequiredExp;
        level++;
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
            playerLevel.Experience = experience;
            playerLevel.level = level;
        }
    }
}

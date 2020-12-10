using UnityEngine;
using UnityEngine.Events;

public class PlayerLevel : MonoBehaviour
{
    public float baseRequiredExp = 100;
    public float increasePerLevel = 5;
    public float level;
    public UnityEvent<float> onExpChanged;
    private float _experience;

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

    public void IncreaseExp(float amount)
    {
        Experience += amount;
    }

    public void IncreaseLevel()
    {
        _experience -= RequiredExp;
        level++;
    }
}

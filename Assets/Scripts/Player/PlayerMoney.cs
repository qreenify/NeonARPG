using System;
using UnityEngine;
using UnityEngine.Events;
[Serializable]
public class PlayerMoney : MonoBehaviour, ISaveable
{
    [SerializeField] private int _moneyAmount;
    [HideInInspector] public UnityEvent<int> onMoneyChanged;

    public int MoneyAmount
    {
        get => _moneyAmount;
        private set
        {
            _moneyAmount = value;
            onMoneyChanged.Invoke(_moneyAmount);
        }
    }
    
    public bool Deserialize(string playerMoney)
    {
       var json = JsonUtility.FromJson<MoneySave>(playerMoney);
       if (json.Equals(new MoneySave())) return false;
       json.ApplyValues(this);
       return true;
    }
    public string Serialize() => JsonUtility.ToJson(new MoneySave(this));

    public void Increase(int amount)
    {
        MoneyAmount += amount;
    }

    public bool TryBuy(int cost)
    {
        if (cost > MoneyAmount) return false;
        MoneyAmount -= cost;
        return true;
    }
    
    [Serializable]
    internal class MoneySave
    {
        public bool Equals(MoneySave other) => moneyAmount == other.moneyAmount;
        public int moneyAmount;
        public MoneySave(){}
        public MoneySave(PlayerMoney playerMoney) => moneyAmount = playerMoney.MoneyAmount;
        public void ApplyValues(PlayerMoney playerMoney) => playerMoney._moneyAmount = moneyAmount;
    }
}
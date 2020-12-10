using UnityEngine;
using UnityEngine.Events;

public class PlayerMoney : MonoBehaviour
{
    private int _moneyAmount;
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
}

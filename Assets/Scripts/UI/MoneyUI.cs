using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public TMP_Text text;

    private void Start()
    {
        var player = PlayerController.playerController;
        if(player == null)
        {
            Destroy(this);
            return;
        }
        if (!player.TryGetComponent(out PlayerMoney playerMoney))
        {
            Destroy(this);
            return;
        }
        playerMoney.onMoneyChanged.AddListener(UpdateText);
        UpdateText(playerMoney.MoneyAmount);
    }
    public void UpdateText(int amount)
    {
        text.SetText($"Money {amount}");
    }
}

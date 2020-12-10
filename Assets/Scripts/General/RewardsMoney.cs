using UnityEngine;

public class RewardsMoney : MonoBehaviour, IReward
{
    public int reward;
    public void Reward()
    {
        if (PlayerController.playerController.TryGetComponent(out PlayerMoney money))
        {
            money.Increase(reward);
        }
    }
}

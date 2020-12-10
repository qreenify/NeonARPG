using UnityEngine;

public class RewardsExp : MonoBehaviour
{
    public float reward;
    public void RewardExp()
    {
        if (PlayerController.playerController.TryGetComponent(out PlayerLevel level))
        {
            level.IncreaseExp(reward);
        }
    }
}

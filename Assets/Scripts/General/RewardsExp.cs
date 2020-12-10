using UnityEngine;

public class RewardsExp : MonoBehaviour, IReward
{
    public float reward;
    public void Reward()
    {
        if (PlayerController.playerController.TryGetComponent(out PlayerLevel level))
        {
            level.Increase(reward);
        }
    }
}

public interface IReward
{
    void Reward();
}

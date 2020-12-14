using UnityEngine;

public class RewardsExp : MonoBehaviour, IReward
{
    public float reward;
    public void Reward()
    {
        PlayerLevel playerLevel = PlayerController.playerController.GetComponent<PlayerLevel>();
        playerLevel.Increase(reward);
    }
}

public interface IReward
{
    void Reward();
}

using UnityEngine;

public class AddPotion : MonoBehaviour
{
    public void AddOnePotion(PlayerController playerController)
    {
        if (playerController.TryGetComponent<HealthPotion>(out var health))
        {
            health.Add();
        }
    }
}

using UnityEngine;

public class AddPotion : MonoBehaviour
{
    public PickupDisplay pickupDisplay;

    public void AddOnePotion(PlayerController playerController)
    {
        if (playerController.TryGetComponent<HealthPotion>(out var health))
        {
            health.Add();
            pickupDisplay.PickupConfirmation();
        }
    }
}

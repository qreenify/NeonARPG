using UnityEngine;

public class AddPotion : MonoBehaviour
{
    public void AddOnePotion(PlayerController playerController)
    {
        if (playerController.TryGetComponent<HealthPotion>(out var health))
        {
            health.Add();
            var pickupDisplay = health.GetComponentInChildren<PickupDisplay>();
            if (pickupDisplay != null)
            {
                pickupDisplay.PickupConfirmation();
            }
        }
    }
}

using TMPro;
using UnityEngine;

public class HealthPotionUI : MonoBehaviour
{
    TextMeshProUGUI tmp;

    private void Awake()
    { 
        tmp = GetComponent<TextMeshProUGUI>();
        if (PlayerController.playerController.TryGetComponent<HealthPotion>(out var healthPotion))
        { 
            healthPotion.onHealthPotionChange.AddListener(UpdatePotionAmount);  
        }
    }

    void UpdatePotionAmount(int i)
    {
        tmp.text = $"Health Potions: {i}";
    }
}

using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Image image;
    public Health health;

    private void Start()
    {
        var player = PlayerController.playerController;
        if(player == null)
        {
            Destroy(this);
            return;
        }
        health = player.GetComponent<Health>();
        if(health == null)
        {
            Destroy(this);
            return;
        }
        health.onHealthChanged.AddListener(UpdateAmount);
        UpdateAmount(health.CurrentHealth);
    }

    public void UpdateAmount(float health)
    {
        image.fillAmount = Mathf.InverseLerp(0, this.health.maxHealth, health);
    }
}

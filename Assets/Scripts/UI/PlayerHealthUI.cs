using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Image image;
    public Health health;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
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
    }

    public void UpdateAmount(float health)
    {
        image.fillAmount = Mathf.InverseLerp(0, this.health.maxHealth, health);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            UpdateAmount(10);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            UpdateAmount(50);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            UpdateAmount(100);
        }
    }
}

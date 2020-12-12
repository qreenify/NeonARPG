using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class HoveringHealthbar : MonoBehaviour
{
    public Transform healthbar;
    private Health health;
    private float originalScale;
    private void Start()
    {
        if (TryGetComponent(out Health health))
        {
            this.health = health;
            originalScale = healthbar.localScale.x;
        }
        else
        {
            Destroy(this);
        }
    }
    public void UpdateHealth(float value)
    {
        healthbar.localScale = new Vector3(Mathf.InverseLerp(0, this.health.maxHealth, value) * originalScale, healthbar.localScale.y);
    }
}

using UnityEngine;

public class HoveringHealthbar : MonoBehaviour
{
    private Health _health;
    private float _originalScale;
    private void Start()
    {
        _health = GetComponentInParent<Health>();
        if (_health != null)
        {
            _originalScale = transform.localScale.x;
            _health.onHealthChanged.AddListener(UpdateHealth);
        }
        else
        {
            Destroy(this);
        }
    }
    public void UpdateHealth(float value)
    {
        transform.localScale = new Vector3(Mathf.InverseLerp(0, this._health.maxHealth, value) * _originalScale, transform.localScale.y);
    }
}

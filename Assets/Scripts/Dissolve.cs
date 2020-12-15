using System;
using System.Collections;
using Unit;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public float dissolveTime = 2.5f;
    [ColorUsage(true, true)]
    public Color dissolveColor = new Color(191, 0, 0, 125);
    [ColorUsage(true, true)]
    public Color condenseColor = new Color(0, 191, 0, 125);

    public IEnumerator DoDissolve()
    {
        if (TryGetComponent<PlayerController>(out var playerController))
        {
            var sensors = FindObjectsOfType<Sensor>();
            foreach (var sensor in sensors)
            {
                sensor.OnPlayerDeath();
            }
        }
        var colliders = GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.material.SetColor("Color_70BD1405", dissolveColor);
        }
        var dissolveAmount = 0.0f;
        while (dissolveAmount < 1.0f)
        {
            dissolveAmount += Time.deltaTime / dissolveTime;
            foreach (var renderer in renderers)
            {
                renderer.material.SetFloat("Vector1_F96551CF", dissolveAmount);
            }

            yield return null;
        }
        GetComponent<Health>().onDefeat.Invoke();
        gameObject.SetActive(false);
    }
    
    public IEnumerator DoCondense()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.material.SetColor("Color_70BD1405", condenseColor);
        }
        var dissolveAmount = 1f;
        while (dissolveAmount > 0.0f)
        {
            dissolveAmount -= Time.deltaTime / dissolveTime;
            foreach (var renderer in renderers)
            {
                renderer.material.SetFloat("Vector1_F96551CF", dissolveAmount);
            }

            yield return null;
        }
        var unit = GetComponent<Unit.Unit>();
        unit.enabled = true;
        unit.Clear();
        var colliders = GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }
        foreach (var renderer in renderers)
        {
            renderer.material.SetColor("Color_70BD1405", condenseColor);
        }
    }
}

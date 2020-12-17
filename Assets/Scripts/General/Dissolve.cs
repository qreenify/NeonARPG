using System.Collections;
using Unit;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public float dissolveTime = 2.5f;
    const float Factor = 32768;
    [ColorUsage(true, true)]
    public Color dissolveColor = new Color(191/255f, 0, 0, 125f/255f) * Factor;
    [ColorUsage(true, true)]
    public Color condenseColor = new Color(0, 191/255f, 0, 125f/255f) * Factor;

    public IEnumerator DoDissolve()
    {
        if (TryGetComponent<PlayerController>(out var playerController))
        {
            playerController.enabled = false;
            playerController.GetComponent<Unit.Unit>().StopMove();
            var sensors = FindObjectsOfType<Sensor>();
            foreach (var sensor in sensors)
            {
                sensor.OnPlayerDeath();
            }
        }
        else
        {
            if (PlayerController.playerController.GetComponent<Unit.Unit>().target == transform)
            {
                PlayerController.playerController.GetComponent<Unit.Unit>().target = null;
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
                if (renderer != null)
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
        if (TryGetComponent<PlayerController>(out var playerController))
        {
            playerController.enabled = true;
        }
    }
}

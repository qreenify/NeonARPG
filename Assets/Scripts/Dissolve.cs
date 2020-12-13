using System;
using System.Collections;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public float dissolveTime = 2.5f;

    public IEnumerator DoDissolve()
    {
        var renderers = GetComponentsInChildren<Renderer>();
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
        gameObject.SetActive(false);
    }
    
    public IEnumerator DoCondense()
    {
        var renderers = GetComponentsInChildren<Renderer>();
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
    }
}

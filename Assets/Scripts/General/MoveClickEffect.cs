using System;
using UnityEngine;

public class MoveClickEffect : MonoBehaviour
{
    public ParticleSystem effect;

    private void Update()
    {
        if (effect.isStopped)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeParticleSystem : MonoBehaviour
{
    public ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.enableEmission = false;
    }

    public void StartParticleSystem()
    {
        particleSystem.enableEmission = true;
    }

    public void StopParticleSystem()
    {
        Debug.Log("goodbye");
        particleSystem.enableEmission = false;
    }
}

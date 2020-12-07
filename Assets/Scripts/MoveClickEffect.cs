﻿using System;
using UnityEngine;

public class MoveClickEffect : MonoBehaviour
{
    public ParticleSystem effect;
    private float _originalSimulationSpeed;
    public float startSimSpeed;

    private void Awake()
    {
        var main = effect.main;
        _originalSimulationSpeed = main.simulationSpeed;
        main.simulationSpeed = startSimSpeed;
    }

    private void Update()
    {
        var main = effect.main;
        main.simulationSpeed = _originalSimulationSpeed;
        if (effect.isStopped)
        {
            Destroy(gameObject);
        }
    }
}

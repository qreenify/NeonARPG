using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class Testing : MonoBehaviour {
    private void Start() {
      // DamagePopup.Create(Vector3.zero, 300);
   }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //bool isCriticalHit = Random.Range(0, 100) < 30;
            //DamagePopup.Create(MouseCursor(),100, isCriticalHit);
        }
    }
}

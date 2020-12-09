using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class DamagePopup : MonoBehaviour
{
    private const float Disappear_Timer_Max = 1f;
    private TextMeshPro textMeshPro;
    private TextMesh textMesh;
    private float disappearTimer;
    private Color textColor;

    private bool IsTextMeshPro => textMeshPro != null;
    
    public void Awake() {
        textMeshPro = transform.GetComponent<TextMeshPro>();
        textMesh = transform.GetComponent<TextMesh>();
    }

    public void Setup(Vector3 position, float damageAmount, bool isCriticalHit)
    {
        transform.position = position;
        if (IsTextMeshPro)
            textMeshPro.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {
            if (IsTextMeshPro)
                textMeshPro.fontSize = 36;
            textColor = Color.yellow;
        }

        else {
            if (IsTextMeshPro)
                textMeshPro.fontSize = 45;
            textColor = Color.red;
        }

        if (IsTextMeshPro)
            textMeshPro.color = textColor;
        else
            textMesh.color = textColor;
        disappearTimer = Disappear_Timer_Max;
    }

    private void Update()
    {
        float moveYSpeed = 20f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        if (disappearTimer < Disappear_Timer_Max * .5f)
        {
            float decreaseScaleAmount = 1.5f;
            transform.localScale += Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            if (IsTextMeshPro)
                textMeshPro.color = textColor;
            else
                textMesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

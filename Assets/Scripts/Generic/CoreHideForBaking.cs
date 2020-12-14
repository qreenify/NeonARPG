using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreHideForBaking : MonoBehaviour
{
    public bool hide = false;

    void OnValidate()
    {
        foreach (var hideObject in FindObjectsOfType<HideForBaking>())
        {
            hideObject.gameObject.SetActive(hide);
        }
    }
}

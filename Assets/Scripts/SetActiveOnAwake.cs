using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnAwake : MonoBehaviour
{
    public bool state;
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(state);
    }
}

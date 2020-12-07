using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantlyDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public bool active;
    void Awake()
    {
        if(active)
            Destroy(gameObject);
    }
}

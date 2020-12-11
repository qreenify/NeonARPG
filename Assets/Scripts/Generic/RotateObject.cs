using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed;
    private Vector3 vSpeed;
    void Start()
    {
        vSpeed = new Vector3(Random.Range(-speed, speed), Random.Range(-speed, speed), Random.Range(-speed, speed));
    }

    void Update()
    {
        transform.Rotate(vSpeed * Time.deltaTime);
    }

    private void OnValidate()
    {
        vSpeed = new Vector3(Random.Range(-speed, speed), Random.Range(-speed, speed), Random.Range(-speed, speed));
    }
}

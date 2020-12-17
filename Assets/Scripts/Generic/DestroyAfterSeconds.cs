using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float time = 1f;

    private void Awake()
    {
        Destroy(gameObject, time);
    }
}

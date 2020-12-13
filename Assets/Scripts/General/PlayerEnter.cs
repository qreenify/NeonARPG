using UnityEngine;
using UnityEngine.Events;

public class PlayerEnter : MonoBehaviour
{
    public UnityEvent<PlayerController> playerEnterEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.TryGetComponent<PlayerController>(out var controller))
        {
            playerEnterEvent.Invoke(controller);
        }
    }
}

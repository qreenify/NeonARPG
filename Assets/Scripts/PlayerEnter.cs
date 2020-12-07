using UnityEngine;
using UnityEngine.Events;

public class PlayerEnter : MonoBehaviour
{
    public UnityEvent<Mover> playerEnterEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Mover>(out var mover))
        {
            playerEnterEvent.Invoke(mover);
        }
    }
}

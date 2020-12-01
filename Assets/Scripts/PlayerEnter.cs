using UnityEngine;
using UnityEngine.Events;

public class PlayerEnter : MonoBehaviour
{
    public UnityEvent playerEnterEvent;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerEnterEvent.Invoke();
        }
    }
}

using UnityEngine;

public class DestroyPlayerOnLoad : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerController.playerController != null)
            Destroy(PlayerController.playerController.gameObject);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Portal otherPortal;
    public Vector3 offset;
    public void Start()
    {
        if(otherPortal != null && gameObject.TryGetComponent<PlayerEnter>(out PlayerEnter playerEnter))
        {
            playerEnter.playerEnterEvent.AddListener(delegate { TeleportToLocation(PlayerController.playerController); });
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void TeleportToLocation(PlayerController controller)
    {
        var position = otherPortal.transform.position;
        var teleportLocation = position + otherPortal.offset;
        controller.navMeshAgent.enabled = false;
        controller.transform.position = teleportLocation;
        controller.navMeshAgent.enabled = true;
        controller.navMeshAgent.destination = teleportLocation;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, otherPortal.transform.position);
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Portal otherPortal;
    public Vector3 offset;
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
}
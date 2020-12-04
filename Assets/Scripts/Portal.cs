using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Portal otherPortal;
    public Vector3 offset;
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void TeleportToLocation(Mover mover)
    {
        var position = otherPortal.transform.position;
        var teleportLocation = position + otherPortal.offset;
        mover.navMeshAgent.enabled = false;
        mover.transform.position = teleportLocation;
        mover.navMeshAgent.enabled = true;
        mover.navMeshAgent.destination = teleportLocation;
    }
}
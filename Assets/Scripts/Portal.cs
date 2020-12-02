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

    public void TeleportToLocation()
    {
        var position = otherPortal.transform.position;
        var teleportLocation = position + otherPortal.offset;
        var mover = FindObjectOfType<Mover>();
        mover.transform.position = teleportLocation;
        mover.navMeshAgent.destination = teleportLocation;
    }
}
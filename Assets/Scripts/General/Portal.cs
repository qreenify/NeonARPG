using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Portal otherPortal;
    public Vector3 offset;
    public bool autoAdd = true;
    [Header("For Switching Scenes")]
    public Vector3 position;
    public void Start()
    {
        if(autoAdd && otherPortal != null && gameObject.TryGetComponent<PlayerEnter>(out PlayerEnter playerEnter))
        {
            playerEnter.playerEnterEvent.AddListener(delegate { TeleportToLocation(PlayerController.playerController); });
        }
    }
    public void LoadScene(string sceneName)
    {
        DontDestroyOnLoad(gameObject);
        PlayerController.playerController.navMeshAgent.enabled = false;
        SceneManager.LoadScene(sceneName);
        TeleportToLocation(position);
        Destroy(gameObject);
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
    
    public void TeleportToLocation(Vector3 position)
    {
        PlayerController.playerController.navMeshAgent.enabled = false;
        PlayerController.playerController.transform.position = position;
        PlayerController.playerController.navMeshAgent.enabled = true;
        PlayerController.playerController.navMeshAgent.destination = position;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, otherPortal.transform.position);
    }
}
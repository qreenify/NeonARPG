using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Portal otherPortal;
    public Vector3 offset;
    public bool autoAdd = true;
    [Header("For Switching Scenes")]
    public Vector3 position;

    public SoundPlayer portalSound;
    float savedTime; //For later
    public float warmUp; //For later /F
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
        //StartCoroutine(LoadAsync(sceneName));
    }

    public void TeleportToLocation(PlayerController controller)
    {
        if (otherPortal == null)
        {
            Debug.LogError("OtherPortal is not assigned either assign it or don't call the method TeleportToLocation(PlayerController controller)");
            return;
        }
        var position = otherPortal.transform.position + otherPortal.offset;
        TeleportToLocation(position);
    }
    
    public void TeleportToLocation(Vector3 position)
    {
        portalSound.enabled = true;
        Debug.Log("Apple");
        if (position == Vector3.zero) return;
        PlayerController.playerController.navMeshAgent.enabled = false;
        PlayerController.playerController.transform.position = position;
        PlayerController.playerController.navMeshAgent.enabled = true;
        PlayerController.playerController.navMeshAgent.destination = position;
    }

    IEnumerator LoadAsync(string sceneName)
    {
        PlayerController.playerController.navMeshAgent.enabled = false;
        var wait = SceneManager.LoadSceneAsync(sceneName);
        while (!wait.isDone)
        {
            yield return null;
        }
        TeleportToLocation(position);
        Destroy(gameObject);
    }
    public void OnDrawGizmos()
    {
        if (otherPortal == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, otherPortal.transform.position);
    }
}
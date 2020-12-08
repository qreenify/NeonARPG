using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Mover : MonoBehaviour {
    [HideInInspector] public NavMeshAgent navMeshAgent;
    Ray cameraToMouseRay;
    new Camera camera;
    public GameObject moveAnimation;
    public GameObject currentAnimation;
    void Start() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        camera = FindObjectOfType<Camera>();
    }
    void Update() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            MoveToClickPoint();
        }
        Debug.DrawRay(cameraToMouseRay.origin, cameraToMouseRay.direction * 500, Color.magenta);
    }

    void MoveToClickPoint() 
    {
        cameraToMouseRay = camera.ScreenPointToRay(Input.mousePosition);
        var hasHit = Physics.Raycast(cameraToMouseRay, out var hitPoint);

        var eventSystem = FindObjectOfType<EventSystem>();
        if (!hasHit || eventSystem != null && eventSystem.IsPointerOverGameObject())
            return;
        navMeshAgent.destination = hitPoint.point;
        if (currentAnimation != null)
        {
            Destroy(currentAnimation);
        }
        currentAnimation = Instantiate(moveAnimation);
        currentAnimation.transform.position = hitPoint.point;
        GetComponent<Unit.Unit>().target = null;
    }
}
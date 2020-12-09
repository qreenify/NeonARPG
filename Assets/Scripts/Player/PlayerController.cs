using System;
using Unit;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Camera camera;
    private Unit.Unit _unit;
    public float maxDistance = 50;
    public UnitAction[] playerActions;
    public bool ranged;
    public KeyCode weaponSwitch;
    public KeyCode lookAroundKey = KeyCode.LeftShift;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    public GameObject moveAnimation;
    public GameObject currentAnimation;
    public static PlayerController playerController;

    private void Awake()
    {
        if (playerController != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            playerController = this;
        }
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _unit = GetComponent<Unit.Unit>();
        SetWeapon();
    }

    private void Update()
    {
        ToggleWeapon();
        Select();
    }

    private void Select()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var eventSystem = FindObjectOfType<EventSystem>();
            if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit) ||
                eventSystem != null && eventSystem.IsPointerOverGameObject()) return;
            if (Vector3.Distance(hit.point, transform.position) > maxDistance) return;
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Enemy") &&
                    hit.collider.gameObject.TryGetComponent(out Health health))
                {
                    var enemy = hit.collider.gameObject;
                    _unit.target = enemy.transform;
                    if (currentAnimation != null)
                    {
                        Destroy(currentAnimation);
                    }

                    var inRange = false;
                    switch (_unit.currentAction)
                    {
                        case Unit.RangedAttack rangedAttack:
                            inRange = rangedAttack.InAttackRange;
                            break;
                        case MeleeAttack meleeAttack:
                            inRange = meleeAttack.InAttackRange;
                            break;
                    }

                    if (!inRange)
                    {
                        navMeshAgent.destination = hit.collider.transform.position;
                    }
                }
            }
        }

        else if (Input.GetMouseButton(1))
        {
            var eventSystem = FindObjectOfType<EventSystem>();
            if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit) || eventSystem != null && eventSystem.IsPointerOverGameObject()) return;
            if (Vector3.Distance(hit.point, transform.position) > maxDistance) return;
            
            _unit.target = null;
            if (currentAnimation != null) 
            { 
                Destroy(currentAnimation);
            }
            currentAnimation = Instantiate(moveAnimation);
            currentAnimation.transform.position = hit.point;
            navMeshAgent.destination = hit.point;
        }
        
        else if (Input.GetKey(lookAroundKey))
        {
            var eventSystem = FindObjectOfType<EventSystem>();
            if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit) ||
                eventSystem != null && eventSystem.IsPointerOverGameObject()) return;
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    private void ToggleWeapon()
    {
        if (Input.GetKeyDown(weaponSwitch))
        {
            ranged = !ranged;
            SetWeapon();
        }
    }

    private void SetWeapon()
    {
        if (ranged)
        {
            for (int i = 0; i < playerActions.Length; i++)
            {
                playerActions[i].enabled = i == (int) PlayerActions.Ranged;
            }
        }
        else
        {
            for (int i = 0; i < playerActions.Length; i++)
            {
                playerActions[i].enabled = i == (int) PlayerActions.Melee;
            }
        }
    }
}

public enum PlayerActions
{
    Melee,
    Ranged
}

using System;
using Unit;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Camera camera;
    public LayerMask playerMask;
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
    public event Action<bool> ONWeaponSwap;
    public event Action<bool> ONHoverOverEnemy;

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
        var eventSystem = FindObjectOfType<EventSystem>();

        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit, Mathf.Infinity, playerMask) || !EventSystem.current.IsPointerOverGameObject())
        {
            var hoverEnemy = hit.collider.gameObject.CompareTag("Enemy") &&
                             hit.collider.gameObject.TryGetComponent(out Health health);

            if (Input.GetMouseButtonDown(0))
            {
                if (Vector3.Distance(hit.point, transform.position) > maxDistance) return;
                if (hit.collider != null)
                {
                    if (hoverEnemy)
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
                            _unit.MoveTo(hit.collider.transform.position);
                        }
                    }
                }
            }

            else if (Input.GetMouseButton(1))
            {
                if (!(Vector3.Distance(hit.point, transform.position) > maxDistance))
                {
                    _unit.target = null;
                    _unit.MoveTo(hit.point);
                }
            }

            else if (Input.GetKey(lookAroundKey))
            {
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                hoverEnemy = true;
            }
            ONHoverOverEnemy?.Invoke(hoverEnemy);

            if (Input.GetMouseButtonUp(1))
            {
                if (currentAnimation != null)
                {
                    Destroy(currentAnimation);
                }
                currentAnimation = Instantiate(moveAnimation);
                currentAnimation.transform.position = hit.point + new Vector3(0, 0.1f, 0);
            }
        }
    }

    private void ToggleWeapon()
    {
        if (Input.GetKeyDown(weaponSwitch))
        {
            ranged = !ranged;
            ONWeaponSwap?.Invoke(ranged);
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

using System;
using Unit;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Camera _camera;
    private Unit.Unit _unit;
    public UnitAction[] playerActions;
    public bool ranged;
    public KeyCode weaponSwitch;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    public GameObject moveAnimation;
    public GameObject currentAnimation;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _camera = Camera.main;
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
        if (!Input.GetMouseButton(0)) return;
        var eventSystem = FindObjectOfType<EventSystem>();
        if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit) || eventSystem != null && eventSystem.IsPointerOverGameObject());
        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            var enemy = hit.collider.gameObject;
            _unit.target = enemy.transform;
            if (currentAnimation != null) 
            { 
                Destroy(currentAnimation);
            }
        }
        else
        {
            _unit.target = null;
            if (currentAnimation != null) 
            { 
                Destroy(currentAnimation);
            }
            currentAnimation = Instantiate(moveAnimation);
            currentAnimation.transform.position = hit.point;
        }

        navMeshAgent.destination = hit.point;
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

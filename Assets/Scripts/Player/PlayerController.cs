﻿using System;
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
    public GameObject targetAnimation;
    public GameObject currentAnimation;
    public static PlayerController playerController;
    public event Action<bool> ONWeaponSwap;
    public event Action<bool, Transform> ONHoverOverEnemy;
    public float timeUntilIdle = 10;
    private float _idleTime;
    
    
    public bool InRange(Transform targetTransform)
    {
        if (ranged)
        {
            var rangedAttack = GetComponent<Unit.RangedAttack>();
            return Vector3.Distance(transform.position, targetTransform.position) < rangedAttack.range;
 
        }
        else
        {
            var meleeAttack = GetComponent<MeleeAttack>();
            return Vector3.Distance(transform.position, targetTransform.position) < meleeAttack.range;
        }
    }
    
    public bool InRange()
    {
        if (ranged)
        {
            var rangedAttack = GetComponent<Unit.RangedAttack>();
            return rangedAttack.InAttackRange;
 
        }
        else
        {
            var meleeAttack = GetComponent<MeleeAttack>();
            return meleeAttack.InAttackRange;
        }
    }
    
    private void Awake()
    {
        if (playerController != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            playerController = this;
        }

        _idleTime = timeUntilIdle;
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
        if (_idleTime > 0)
            _idleTime -= Time.deltaTime;
        else if (TryGetComponent<MeleeAttack>(out var meleeAttack))
        {
            meleeAttack.animator.SetBool("Idle", true);
        }
    }

    private void Select()
    {
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit, Mathf.Infinity, playerMask) || !EventSystem.current.IsPointerOverGameObject())
        {
            var hoverEnemy = false;
            Transform hoverTransform = null;
            if (hit.collider != null)
            {
              hoverEnemy = hit.collider.gameObject.CompareTag("Enemy") && hit.collider.gameObject.TryGetComponent(out Health health);
              hoverTransform = hit.transform;
              var attackRangeDebug = hit.collider.GetComponentInChildren<AttackRangeDebug>();
              if (attackRangeDebug != null)
              {
                  attackRangeDebug.Toggle(true, transform);
              }
            }
            

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

                        currentAnimation = Instantiate(targetAnimation, enemy.transform);
                        if (!InRange())
                        {
                            _unit.MoveTo(hit.collider.transform.position);
                        }
                    }
                }

                _idleTime = timeUntilIdle;
                if (TryGetComponent<MeleeAttack>(out var meleeAttack))
                {
                    meleeAttack.animator.SetBool("Idle", false);
                }
            }

            else if (Input.GetMouseButton(1))
            {
                if (!(Vector3.Distance(hit.point, transform.position) > maxDistance))
                {
                    _unit.target = null;
                    _unit.MoveTo(hit.point);
                }
                _idleTime = timeUntilIdle;
                if (TryGetComponent<MeleeAttack>(out var meleeAttack))
                {
                    meleeAttack.animator.SetBool("Idle", false);
                }
            }

            else if (Input.GetKey(lookAroundKey))
            {
                _unit.StopMove();
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                hoverEnemy = true;
                _idleTime = timeUntilIdle;
                if (TryGetComponent<MeleeAttack>(out var meleeAttack))
                {
                    meleeAttack.animator.SetBool("Idle", false);
                }
            }
            ONHoverOverEnemy?.Invoke(hoverEnemy, hoverTransform);

            if (Input.GetMouseButtonUp(1))
            {
                if (currentAnimation != null)
                {
                    Destroy(currentAnimation);
                }
                currentAnimation = Instantiate(moveAnimation);
                currentAnimation.transform.position = hit.point + new Vector3(0, 0.1f, 0);
                _idleTime = timeUntilIdle;
                if (TryGetComponent<MeleeAttack>(out var meleeAttack))
                {
                    meleeAttack.animator.SetBool("Idle", false);
                }
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
                _unit.currentAction = null;
            }
        }
        else
        {
            for (int i = 0; i < playerActions.Length; i++)
            {
                playerActions[i].enabled = i == (int) PlayerActions.Melee;
                _unit.currentAction = null;
            }
        }
    }
}

public enum PlayerActions
{
    Melee,
    Ranged
}

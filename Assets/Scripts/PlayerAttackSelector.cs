using Unit;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttackSelector : MonoBehaviour
{
    private Camera _camera;
    public bool ranged;
    public KeyCode weaponSwitch;

    private void Start()
    {
        _camera = Camera.main;
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
            GetComponent<Unit.Unit>().target = enemy.transform;
            Destroy(GetComponent<Mover>().currentAnimation); 
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
            GetComponent<Unit.Unit>().currentAction = GetComponent<UnitRangedAttack>();
        }
        else
        {
            GetComponent<Unit.Unit>().currentAction = GetComponent<UnitMeleeAttack>();
        }
    }
}

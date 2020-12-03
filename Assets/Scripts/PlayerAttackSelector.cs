using Unit;
using UnityEngine;

public class PlayerAttackSelector : MonoBehaviour
{
    Camera _camera;
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
        if (!Input.GetMouseButton(0)) return;
        if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit)) return;
        if (!hit.collider.gameObject.CompareTag("Enemy")) return;
        var enemy = hit.collider.gameObject;
        GetComponent<Unit.Unit>().target = enemy.transform;
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

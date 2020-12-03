using UnityEngine;
using unit = Unit.Unit;

public class PlayerAttackSelector : MonoBehaviour
{
    Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0)) return;
        if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit)) return;
        if (!hit.collider.gameObject.CompareTag("Enemy")) return;
        var enemy = hit.collider.gameObject;
        GetComponent<unit>().target = enemy.transform;
    }
}

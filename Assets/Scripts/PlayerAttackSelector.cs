using UnityEngine;

public class PlayerAttackSelector : MonoBehaviour
{
    Camera _camera;
    public bool ranged;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!ranged)
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit)) return;
            if (!hit.collider.gameObject.CompareTag("Enemy")) return;
            var enemy = hit.collider.gameObject;
            GetComponent<MeleeAttack>().Attack(enemy);
        }
        else
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit)) return;
            GetComponent<PlayerRanged>().TrySpawnProjectile(hit.point);
        }
    }
}

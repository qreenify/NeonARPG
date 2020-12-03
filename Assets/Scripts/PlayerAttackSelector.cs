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
            if (!Input.GetMouseButton(0)) return;
            if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit)) return;
            if (!hit.collider.gameObject.CompareTag("Enemy")) return;
            var enemy = hit.collider.gameObject;
            GetComponent<PlayerMeleeAttack>().Attack(enemy);
        }
        else
        {
            if (!Input.GetMouseButton(0)) return;
            if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit)) return;
            GetComponent<PlayerRanged>().TrySpawnProjectile(hit.point);
        }
    }
}

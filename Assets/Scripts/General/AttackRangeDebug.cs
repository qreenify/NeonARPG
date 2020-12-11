using Unit;
using UnityEngine;

public class AttackRangeDebug : MonoBehaviour
{
    public Transform parent;
    public Transform attackRange;
    public SpriteRenderer spriteRenderer;
    public float rotateSpeed;

    private void Start()
    {
        PlayerController.playerController.ONWeaponSwap += ChangeWeapon;
        PlayerController.playerController.ONHoverOverEnemy += Toggle;
    }

    private void LateUpdate()
    {
        parent.rotation = Quaternion.identity;
        attackRange.transform.Rotate(Vector3.back * (rotateSpeed * Time.deltaTime));
    }

    private void Toggle(bool showAttackRange)
    {
        if (attackRange == null || spriteRenderer == null) return;
        spriteRenderer.enabled = showAttackRange;
    }

    private void ChangeWeapon(bool ranged)
    {
        if (attackRange == null) return;
        if (ranged)
        {
            var rangedAttack = GetComponent<Unit.RangedAttack>();
            attackRange.localScale = new Vector3(rangedAttack.range, rangedAttack.range, attackRange.localScale.z);
        }
        else
        {
            var meleeAttack = GetComponent<MeleeAttack>();
            attackRange.localScale = new Vector3(meleeAttack.range, meleeAttack.range, attackRange.localScale.z);
        }
    }
}

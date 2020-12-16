using System;
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
        var isPlayerController = GetComponentInParent<PlayerController>();
        if (isPlayerController != null)
        {
           PlayerController.playerController.ONWeaponSwap += ChangeWeapon; 
        }
        PlayerController.playerController.ONHoverOverEnemy += Toggle;
    }

    private void LateUpdate()
    {
        //parent.rotation = Quaternion.identity;
        attackRange.transform.Rotate(Vector3.back * (rotateSpeed * Time.deltaTime));
    }

    public void Toggle(bool showAttackRange, Transform targetTransform)
    {
        if (attackRange == null || spriteRenderer == null) return;
        spriteRenderer.enabled = showAttackRange;
    }

    private void ChangeWeapon(bool ranged)
    {
        if (attackRange == null) return;
        if (ranged)
        {
            var rangedAttack = GetComponentInParent<Unit.RangedAttack>();
            attackRange.localScale = new Vector3(rangedAttack.range, rangedAttack.range, attackRange.localScale.z);
        }
        else
        {
            var meleeAttack = GetComponentInParent<MeleeAttack>();
            attackRange.localScale = new Vector3(meleeAttack.range, meleeAttack.range, attackRange.localScale.z);
        }
    }

    private void OnValidate()
    {
        if (attackRange == null) return;
        {
            var meleeAttack = GetComponentInParent<MeleeAttack>();
            var rangedAttack = GetComponentInParent<Unit.RangedAttack>();
            if (meleeAttack != null)
            {
               attackRange.localScale = new Vector3(meleeAttack.range, meleeAttack.range, attackRange.localScale.z);
            }
            else if (rangedAttack != null)
            {
                attackRange.localScale = new Vector3(rangedAttack.range, rangedAttack.range, attackRange.localScale.z);
            }
        }
    }
}

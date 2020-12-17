using Unit;
using UnityEngine;

public class AttackRangeDebug : MonoBehaviour
{
    public Transform parent;
    public Transform attackRange;
    public SpriteRenderer spriteRenderer;
    public float rotateSpeed;
    private float _timeToShowFor;
    [SerializeField] private bool isPlayer;

    private void Start()
    {
        if (isPlayer)
        {
           PlayerController.playerController.ONWeaponSwap += ChangeWeapon;
           PlayerController.playerController.ONHoverOverEnemy += Toggle;
        }

        var thisScale = parent.localScale;
        var parentScale = transform.parent.localScale;
        thisScale = new Vector3(1 / parentScale.x, 1 / parentScale.y, 1 / parentScale.z) * 1.05f;
        parent.localScale = thisScale;
        attackRange.transform.localPosition = new Vector3(attackRange.transform.localPosition.x, -parentScale.x + 0.2f);
    }

    private void LateUpdate()
    {
        //parent.rotation = Quaternion.identity;
        if (!isPlayer)
        {
           if (_timeToShowFor > 0)
           {
               _timeToShowFor -= Time.deltaTime;
           }
           else
           {
               spriteRenderer.enabled = false;
               _timeToShowFor = 0;
           } 
        }
        attackRange.transform.Rotate(Vector3.back * (rotateSpeed * Time.deltaTime));
    }

    public void Toggle(bool showAttackRange, Transform targetTransform)
    {
        if (attackRange == null || spriteRenderer == null) return;
        if (!isPlayer)
        {
            _timeToShowFor += Time.deltaTime;
        }
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
